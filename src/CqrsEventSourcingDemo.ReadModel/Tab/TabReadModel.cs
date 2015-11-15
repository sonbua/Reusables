using System;
using System.Linq;
using CqrsEventSourcingDemo.Event.Tab;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class TabReadModel : IQueryHandler<ActiveTableNumbersQuery, int[]>,
                                IQueryHandler<TabIdForTableQuery, Guid>,
                                IEventSubscriber<TabOpened>,
                                IEventSubscriber<DrinkOrdered>,
                                IEventSubscriber<FoodOrdered>
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

        private Tab GetTabById(Guid id)
        {
            return _database.Set<Tab>().Single(tab => tab.Id == id);
        }
    }
}
