using System;
using Reusables.Cqrs;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace CqrsEventSourcingDemo.Web.Domain.Commands.Tab
{
    public class TabService : ICommandHandler<OpenTab>
    {
        private readonly IEventStore _eventStore;

        public TabService(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(OpenTab command)
        {
            var id = Guid.NewGuid();

            _eventStore.Act<TabAggregate>(id, aggregate => aggregate.OpenTab(id, command.TableNumber, command.Waiter));

            command.Id = id;
        }
    }
}
