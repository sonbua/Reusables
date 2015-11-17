using System;
using MailingServiceDemo.AggregateRoot;
using Reusables.Cqrs;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace MailingServiceDemo.Command
{
    public class MailingService : ICommandHandler<SendMail>
    {
        private readonly IEventStore _eventStore;

        public MailingService(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(SendMail command)
        {
            var id = Guid.NewGuid();

            _eventStore.Act<MailingAggregate>(id, aggregate => aggregate.SendMail(id, command.Messages, command.Priority));

            command.Id = id;
        }
    }
}
