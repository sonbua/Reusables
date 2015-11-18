using System;
using System.Linq;
using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using MailingServiceDemo.ReadModel;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class MesasgeStore : IEventSubscriber<MailRequestReceived>,
                                IEventSubscriber<MessageSent>,
                                IEventSubscriber<SendingFailed>,
                                IEventSubscriber<AnalysisRequired>
    {
        private readonly IViewModelDatabase _database;
        private readonly IEventPublisher _eventPublisher;
        private readonly IApplicationSettings _settings;

        public MesasgeStore(IViewModelDatabase database, IEventPublisher eventPublisher, IApplicationSettings settings)
        {
            _database = database;
            _eventPublisher = eventPublisher;
            _settings = settings;
        }

        public void Handle(MailRequestReceived @event)
        {
            foreach (var mailMessage in @event.Messages)
            {
                var outboxMessage = new OutboxMessage
                                    {
                                        Id = Guid.NewGuid(),
                                        RequestId = @event.Id,
                                        Message = mailMessage,
                                        Priority = @event.Priority,
                                        QueuedAt = DateTime.UtcNow
                                    };

                _database.Set<OutboxMessage>().Add(outboxMessage);
            }

            _eventPublisher.Publish(new MessageQueued());
        }

        public void Handle(MessageSent @event)
        {
            _database.Set<SentMessage>()
                     .Add(new SentMessage
                          {
                              Id = @event.MessageId,
                              Message = @event.Message,
                              SentAt = @event.SentAt
                          });

            _database.Set<OutboxMessage>()
                     .Remove(@event.MessageId);
        }

        public void Handle(SendingFailed @event)
        {
            _database.Set<FaultMessage>().Add(new FaultMessage
                                              {
                                                  MessageId = @event.MessageId,
                                                  Message = @event.Message,
                                                  Reason = @event.Reason,
                                                  TriedAt = @event.TriedAt
                                              });

            var attemptCount = _database.Set<FaultMessage>().Count(message => message.MessageId == @event.MessageId);

            if (attemptCount < _settings.MaxAttempt)
            {
                _eventPublisher.Publish(new MessageQueued());

                return;
            }

            _eventPublisher.Publish(new AnalysisRequired
                                    {
                                        MessageId = @event.MessageId,
                                        Message = @event.Message
                                    });
        }

        public void Handle(AnalysisRequired @event)
        {
            _database.Set<SuspiciousMessage>()
                     .Add(new SuspiciousMessage
                          {
                              MessageId = @event.MessageId,
                              Message = @event.Message
                          });
        }
    }
}
