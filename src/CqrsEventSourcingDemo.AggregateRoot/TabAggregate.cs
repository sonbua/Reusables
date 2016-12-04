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
        private readonly List<OrderedItem> _preparedFood = new List<OrderedItem>();

        private bool _open;
        private decimal _servedItemsValue;

        public TabAggregate(IEnumerable<object> history)
        {
            foreach (var @event in history)
                Apply(@event);
        }

        public void OpenTab(Guid id, int tableNumber, string waiter)
        {
            ApplyUncommittedEvent(
                new TabOpened
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
                ApplyUncommittedEvent(
                    new DrinkOrdered
                    {
                        TabId = Id,
                        Items = drinks
                    });

            var food = orderedItems.FindAll(item => !item.IsDrink);

            if (food.Any())
                ApplyUncommittedEvent(
                    new FoodOrdered
                    {
                        TabId = Id,
                        Items = food
                    });
        }

        public void MarkDrinkServed(List<int> menuNumbers)
        {
            ApplyUncommittedEvent(
                new DrinksServed
                {
                    TabId = Id,
                    MenuNumbers = menuNumbers
                });
        }

        public void MarkFoodPrepared(List<int> menuNumbers)
        {
            ApplyUncommittedEvent(
                new FoodPrepared
                {
                    TabId = Id,
                    MenuNumbers = menuNumbers
                });
        }

        public void MarkFoodServed(List<int> menuNumbers)
        {
            ApplyUncommittedEvent(
                new FoodServed
                {
                    TabId = Id,
                    MenuNumbers = menuNumbers
                });
        }

        public void CloseTab(decimal amountPaid)
        {
            if (!_open)
                throw new TabNotOpenException();
            if (HasUnservedItems())
                throw new TabHasUnservedItemsException();
            if (!EnoughPaid(amountPaid))
                throw new MustPayEnoughException();

            ApplyUncommittedEvent(
                new TabClosed
                {
                    TabId = Id,
                    AmountPaid = amountPaid,
                    OrderValue = _servedItemsValue,
                    TipValue = amountPaid - _servedItemsValue
                });
        }

        private bool HasUnservedItems()
        {
            return _outstandingDrinks.Any() || _outstandingFood.Any() || _preparedFood.Any();
        }

        private bool EnoughPaid(decimal amountPaid)
        {
            return amountPaid >= _servedItemsValue;
        }

        private void ApplyUncommittedEvent<TEvent>(TEvent @event)
        {
            UncommittedEvents.Add(@event);

            Apply(@event);
        }

        private void Apply<TEvent>(TEvent @event)
        {
            Version++;

            this.ApplyInternally(@event);
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
            foreach (var menuNumber in @event.MenuNumbers)
            {
                var servedDrink = _outstandingDrinks.First(item => item.MenuNumber == menuNumber);

                _outstandingDrinks.Remove(servedDrink);
                _servedItemsValue += servedDrink.Price;
            }
        }

        private void When(FoodPrepared @event)
        {
            foreach (var menuNumber in @event.MenuNumbers)
            {
                var preparedFood = _outstandingFood.First(item => item.MenuNumber == menuNumber);

                _outstandingFood.Remove(preparedFood);
                _preparedFood.Add(preparedFood);
            }
        }

        private void When(FoodServed @event)
        {
            foreach (var menuNumber in @event.MenuNumbers)
            {
                var servedFood = _preparedFood.First(item => item.MenuNumber == menuNumber);

                _preparedFood.Remove(servedFood);
                _servedItemsValue += servedFood.Price;
            }
        }

        private void When(TabClosed @event)
        {
            _open = false;
        }
    }
}