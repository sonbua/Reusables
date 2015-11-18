using System;
using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using MailingServiceDemo.ReadModel;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class OutboxMessageRepo : IEventSubscriber<MailRequestReceived>,
                                     IEventSubscriber<MessageSent>
    {
        private readonly IViewModelDatabase _database;
        private readonly IEventPublisher _eventPublisher;

        public OutboxMessageRepo(IViewModelDatabase database, IEventPublisher eventPublisher)
        {
            _database = database;
            _eventPublisher = eventPublisher;
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
            _database.Set<OutboxMessage>()
                     .Remove(@event.MessageId);
        }
    }
}
