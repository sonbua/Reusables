using System;
using CqrsEventSourcingDemo.AggregateRoot;
using Reusables.Cqrs;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace CqrsEventSourcingDemo.Command.Tab
{
    public class TabService :
        ICommandHandler<OpenTab>,
        ICommandHandler<PlaceOrder>,
        ICommandHandler<MarkDrinksServed>,
        ICommandHandler<MarkFoodPrepared>,
        ICommandHandler<MarkFoodServed>,
        ICommandHandler<CloseTab>
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

        public void Handle(MarkFoodPrepared command)
        {
            _eventStore.Act<TabAggregate>(command.TabId, aggregate => aggregate.MarkFoodPrepared(command.MenuNumbers));
        }

        public void Handle(MarkFoodServed command)
        {
            _eventStore.Act<TabAggregate>(command.TabId, aggregate => aggregate.MarkFoodServed(command.MenuNumbers));
        }

        public void Handle(CloseTab command)
        {
            _eventStore.Act<TabAggregate>(command.TabId, aggregate => aggregate.CloseTab(command.AmountPaid));
        }
    }
}