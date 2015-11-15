using System;
using System.Collections.Generic;
using CqrsEventSourcingDemo.Event.Tab;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace CqrsEventSourcingDemo.AggregateRoot
{
    public class TabAggregate : Aggregate
    {
        private readonly List<OrderedItem> _outstandingDrinks = new List<OrderedItem>();
        private readonly List<OrderedItem> _outstandingFood = new List<OrderedItem>();
        private bool _open;

        public TabAggregate(IEnumerable<object> history)
        {
            foreach (var @event in history)
            {
                Apply(@event);
            }
        }

        public void OpenTab(Guid id, int tableNumber, string waiter)
        {
            Publish(new TabOpened
                    {
                        Id = id,
                        TableNumber = tableNumber,
                        Waiter = waiter
                    });
        }

        public void PlaceOrder(Guid tabId, List<OrderedItem> orderedItems)
        {
            Publish(new DrinkOrdered
                    {
                        TabId = tabId,
                        Items = orderedItems.FindAll(item => item.IsDrink)
                    });

            Publish(new FoodOrdered
                    {
                        TabId = tabId,
                        Items = orderedItems.FindAll(item => !item.IsDrink)
                    });
        }

        private void Publish<TEvent>(TEvent @event)
        {
            UncommittedEvents.Add(@event);

            Apply(@event);
        }

        private void Apply<TEvent>(TEvent @event)
        {
            Version++;

            this.Replay(@event);
        }

        private void When(TabOpened @event)
        {
            _open = true;
        }

        private void When(DrinkOrdered @event)
        {
            _outstandingDrinks.AddRange(@event.Items);
        }

        private void When(FoodOrdered @event)
        {
            _outstandingFood.AddRange(@event.Items);
        }
    }
}
