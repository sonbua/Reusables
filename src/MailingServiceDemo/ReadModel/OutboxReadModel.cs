using System;
using MailingServiceDemo.Event;
using Reusables.EventSourcing;

namespace MailingServiceDemo.ReadModel
{
    public class OutboxReadModel : IEventSubscriber<MailRequestReceived>
    {
        private readonly IViewModelDatabase _database;

        public OutboxReadModel(IViewModelDatabase database)
        {
            _database = database;
        }

        public void Handle(MailRequestReceived @event)
        {
            foreach (var mailMessage in @event.Messages)
            {
                _database.Set<OutboxMessage>().Add(new OutboxMessage
                                                   {
                                                       Id = Guid.NewGuid(),
                                                       RequestId = @event.Id,
                                                       MailMessage = mailMessage
                                                   });
            }
        }
    }
}
