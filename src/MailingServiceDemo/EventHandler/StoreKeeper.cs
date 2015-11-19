using System;
using System.Linq;
using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using MailingServiceDemo.Model;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class StoreKeeper : IEventSubscriber<MailRequestReceived>,
                               IEventSubscriber<MessageSent>,
                               IEventSubscriber<SendingFailed>,
                               IEventSubscriber<AnalysisRequired>
    {
        private readonly IDbContext _dbContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly IApplicationSettings _settings;

        public StoreKeeper(IDbContext dbContext, IEventPublisher eventPublisher, IApplicationSettings settings)
        {
            _dbContext = dbContext;
            _eventPublisher = eventPublisher;
            _settings = settings;
        }

        public void Handle(MailRequestReceived @event)
        {
            foreach (var mailMessage in @event.Messages)
            {
                _dbContext.Set<OutboxMessage>()
                          .Add(new OutboxMessage
                               {
                                   Id = Guid.NewGuid(),
                                   RequestId = @event.Id,
                                   Message = mailMessage,
                                   Priority = (int) mailMessage.Priority,
                                   QueuedAt = DateTime.UtcNow
                               });
            }

            _eventPublisher.Publish(new OutboxManagementNeeded());
        }

        public void Handle(MessageSent @event)
        {
            _dbContext.Set<SentMessage>()
                      .Add(new SentMessage
                           {
                               Id = @event.MessageId,
                               RequestId = @event.RequestId,
                               Message = @event.Message,
                               SentAt = @event.SentAt
                           });

            _dbContext.Set<OutboxMessage>()
                      .Remove(@event.MessageId);
        }

        public void Handle(SendingFailed @event)
        {
            _dbContext.Set<FaultMessage>()
                      .Add(new FaultMessage
                           {
                               RequestId = @event.RequestId,
                               MessageId = @event.MessageId,
                               Message = @event.Message,
                               Reason = @event.Reason,
                               TriedAt = @event.TriedAt
                           });

            var attemptCount = _dbContext.Set<FaultMessage>().Count(message => message.MessageId == @event.MessageId);

            if (attemptCount < _settings.MaxAttempt)
            {
                _eventPublisher.Publish(new OutboxManagementNeeded());

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
            _dbContext.Set<SuspiciousMessage>()
                      .Add(new SuspiciousMessage
                           {
                               MessageId = @event.MessageId,
                               Message = @event.Message
                           });
        }
    }
}
