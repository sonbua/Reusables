using System;
using System.Collections.Generic;
using System.Linq;
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

        public void PlaceOrder(List<OrderedItem> orderedItems)
        {
            var drinks = orderedItems.FindAll(item => item.IsDrink);

            if (drinks.Any())
            {
                Publish(new DrinkOrdered
                        {
                            TabId = Id,
                            Items = drinks
                        });
            }

            var food = orderedItems.FindAll(item => !item.IsDrink);

            if (food.Any())
            {
                Publish(new FoodOrdered
                        {
                            TabId = Id,
                            Items = food
                        });
            }
        }

        public void MarkDrinkServed(List<int> menuNumbers)
        {
            Publish(new DrinksServed
                    {
                        TabId = Id,
                        MenuNumbers = menuNumbers
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

            this.ApplyEventOptionally(@event);
        }

        private void When(TabOpened @event)
        {
            Id = @event.Id;
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

        private void When(DrinksServed @event)
        {
            foreach (var drinkMenuNumber in @event.MenuNumbers)
            {
                var drinkToRemove = _outstandingDrinks.FirstOrDefault(drink => drink.MenuNumber == drinkMenuNumber);

                _outstandingDrinks.Remove(drinkToRemove);
            }
        }
    }
}
