using System;
using System.Collections.Generic;
using System.Linq;
using CqrsEventSourcingDemo.Event.Tab;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class TabReadModel :
        IQueryHandler<ActiveTableNumbers, int[]>,
        IQueryHandler<TabIdForTable, Guid>,
        IQueryHandler<TabForTable, TabStatus>,
        IQueryHandler<TodoListForWaiter, IDictionary<int, TabItem[]>>,
        IEventSubscriber<TabOpened>,
        IEventSubscriber<DrinkOrdered>,
        IEventSubscriber<FoodOrdered>,
        IEventSubscriber<DrinksServed>,
        IEventSubscriber<FoodPrepared>,
        IEventSubscriber<FoodServed>,
        IEventSubscriber<TabClosed>
    {
        private readonly IReadModelDatabase _database;

        public TabReadModel(IReadModelDatabase database)
        {
            _database = database;
        }

        public int[] Handle(ActiveTableNumbers query)
        {
            return _database.Set<Tab>().Select(x => x.TableNumber).ToArray();
        }

        public Guid Handle(TabIdForTable query)
        {
            return _database.Set<Tab>()
                .Single(tab =>
                    tab.TableNumber == query.TableNumber &&
                    tab.Status == TabStatuses.Open)
                .Id;
        }

        public TabStatus Handle(TabForTable query)
        {
            var tab = _database.Set<Tab>().Single(x => x.TableNumber == query.TableNumber);

            return new TabStatus
            {
                TabId = tab.Id,
                TableNumber = tab.TableNumber,
                ToServe = tab.ToServe,
                InPreparation = tab.InPreparation,
                Served = tab.Served
            };
        }

        public IDictionary<int, TabItem[]> Handle(TodoListForWaiter query)
        {
            return _database.Set<Tab>()
                .Where(tab => tab.Status == TabStatuses.Open)
                .Where(tab => tab.Waiter == query.StaffId)
                .ToDictionary(tab => tab.TableNumber, tab => tab.ToServe.ToArray());
        }

        public void Handle(TabOpened @event)
        {
            _database.Set<Tab>()
                .Add(new Tab
                {
                    Id = @event.Id,
                    TableNumber = @event.TableNumber,
                    Waiter = @event.Waiter,
                });
        }

        public void Handle(DrinkOrdered @event)
        {
            var tab = _database.Set<Tab>().GetById(@event.TabId);
            var drinksToServe =
                @event.Items.Select(item =>
                    new TabItem
                    {
                        MenuNumber = item.MenuNumber,
                        Description = item.Description,
                        Price = item.Price
                    });

            tab.ToServe.AddRange(drinksToServe);
        }

        public void Handle(FoodOrdered @event)
        {
            var tab = _database.Set<Tab>().GetById(@event.TabId);
            var foodInPreparation =
                @event.Items.Select(item =>
                    new TabItem
                    {
                        MenuNumber = item.MenuNumber,
                        Description = item.Description,
                        Price = item.Price
                    });

            tab.InPreparation.AddRange(foodInPreparation);
        }

        public void Handle(DrinksServed @event)
        {
            var tab = _database.Set<Tab>().GetById(@event.TabId);

            foreach (var drinkMenuNumber in @event.MenuNumbers)
            {
                var servedDrink = tab.ToServe.First(drink => drink.MenuNumber == drinkMenuNumber);

                tab.ToServe.Remove(servedDrink);
                tab.Served.Add(servedDrink);
            }
        }

        public void Handle(FoodPrepared @event)
        {
            var tab = _database.Set<Tab>().GetById(@event.TabId);

            foreach (var menuNumber in @event.MenuNumbers)
            {
                var preparedFood = tab.InPreparation.First(item => item.MenuNumber == menuNumber);

                tab.InPreparation.Remove(preparedFood);
                tab.ToServe.Add(preparedFood);
            }
        }

        public void Handle(FoodServed @event)
        {
            var tab = _database.Set<Tab>().GetById(@event.TabId);

            foreach (var menuNumber in @event.MenuNumbers)
            {
                var servedFood = tab.ToServe.First(item => item.MenuNumber == menuNumber);

                tab.ToServe.Remove(servedFood);
                tab.Served.Add(servedFood);
            }
        }

        public void Handle(TabClosed @event)
        {
            _database.Set<Tab>().Remove(@event.TabId);
        }
    }
}