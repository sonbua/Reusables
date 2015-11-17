using System;
using MailingServiceDemo.AggregateRoot;
using Reusables.Cqrs;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace MailingServiceDemo.Command
{
    public class MailService : ICommandHandler<SendMail>
    {
        private readonly IEventStore _eventStore;

        public MailService(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(SendMail command)
        {
            var id = Guid.NewGuid();

            _eventStore.Act<MailAggregate>(id, aggregate => aggregate.SendMail(id, command.Messages, command.Priority));

            command.Id = id;
        }
    }
}
