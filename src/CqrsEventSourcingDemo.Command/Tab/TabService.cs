using System;
using CqrsEventSourcingDemo.AggregateRoot;
using Reusables.Cqrs;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace CqrsEventSourcingDemo.Command.Tab
{
    public class TabService : ICommandHandler<OpenTab>,
                              ICommandHandler<PlaceOrder>,
                              ICommandHandler<MarkDrinksServed>
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

        public void Handle(PlaceOrder command)
        {
            _eventStore.Act<TabAggregate>(command.TabId, aggregate => aggregate.PlaceOrder(command.Items));
        }

        public void Handle(MarkDrinksServed command)
        {
            _eventStore.Act<TabAggregate>(command.TabId, aggregate => aggregate.MarkDrinkServed(command.MenuNumbers));
        }
    }
}
