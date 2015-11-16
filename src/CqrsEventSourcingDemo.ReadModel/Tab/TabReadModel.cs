﻿using System;
using System.Linq;
using CqrsEventSourcingDemo.Event.Tab;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class TabReadModel : IQueryHandler<ActiveTableNumbersQuery, int[]>,
                                IQueryHandler<TabIdForTableQuery, Guid>,
                                IQueryHandler<TabForTableQuery, TabStatus>,
                                IEventSubscriber<TabOpened>,
                                IEventSubscriber<DrinkOrdered>,
                                IEventSubscriber<FoodOrdered>,
                                IEventSubscriber<DrinksServed>
    {
        private readonly IViewModelDatabase _database;

        public TabReadModel(IViewModelDatabase database)
        {
            _database = database;
        }

        public int[] Handle(ActiveTableNumbersQuery query)
        {
            return _database.Set<Tab>().Select(x => x.TableNumber).ToArray();
        }

        public Guid Handle(TabIdForTableQuery query)
        {
            return _database.Set<Tab>()
                            .Single(tab => tab.TableNumber == query.TableNumber &&
                                           tab.Status == TabStatuses.Open)
                            .Id;
        }

        public TabStatus Handle(TabForTableQuery query)
        {
            var tab = _database.Set<Tab>().Single(x => x.TableNumber == query.TableNumber &&
                                                       x.Status == TabStatuses.Open);

            return new TabStatus
                   {
                       TabId = tab.Id,
                       TableNumber = tab.TableNumber,
                       ToServe = tab.ToServe,
                       InPreparation = tab.InPreparation,
                       Served = tab.Served
                   };
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
            var tab = GetTabById(@event.TabId);
            var drinksToServe = @event.Items.Select(item => new TabItem
                                                            {
                                                                MenuNumber = item.MenuNumber,
                                                                Description = item.Description,
                                                                Price = item.Price
                                                            });

            tab.ToServe.AddRange(drinksToServe);
        }

        public void Handle(FoodOrdered @event)
        {
            var tab = GetTabById(@event.TabId);
            var foodInPreparation = @event.Items.Select(item => new TabItem
                                                                {
                                                                    MenuNumber = item.MenuNumber,
                                                                    Description = item.Description,
                                                                    Price = item.Price
                                                                });

            tab.InPreparation.AddRange(foodInPreparation);
        }

        public void Handle(DrinksServed @event)
        {
            var tab = GetTabById(@event.TabId);

            foreach (var drinkMenuNumber in @event.MenuNumbers)
            {
                var servedDrink = tab.ToServe.FirstOrDefault(drink => drink.MenuNumber == drinkMenuNumber);

                tab.ToServe.Remove(servedDrink);

                tab.Served.Add(servedDrink);
            }
        }

        private Tab GetTabById(Guid id)
        {
            return _database.Set<Tab>().Single(tab => tab.Id == id);
        }
    }
}
