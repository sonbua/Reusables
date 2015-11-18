using System;
using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using MailingServiceDemo.ReadModel;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class OutboxReadModel : IEventSubscriber<MailRequestReceived>
    {
        private readonly IViewModelDatabase _database;
        private readonly IEventPublisher _eventPublisher;

        public OutboxReadModel(IViewModelDatabase database, IEventPublisher eventPublisher)
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
                                        Message = mailMessage
                                    };

                _database.Set<OutboxMessage>().Add(outboxMessage);

                _eventPublisher.Publish(new MessageQueued
                                        {
                                            MessageId = outboxMessage.Id,
                                            Message = outboxMessage.Message
                                        });
            }
        }
    }
}
