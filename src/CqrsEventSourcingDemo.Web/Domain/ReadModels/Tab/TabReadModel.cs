using System.Linq;
using CqrsEventSourcingDemo.Web.Abstractions.Views;
using CqrsEventSourcingDemo.Web.Domain.Events.Tab;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Domain.ReadModels.Tab
{
    public class TabReadModel : IQueryHandler<ActiveTableNumbersQuery, int[]>,
                                IEventSubscriber<TabOpened>
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

        public void Handle(TabOpened @event)
        {
            _database.Set<Tab>().Add(new Tab
                                     {
                                         Id = @event.Id,
                                         TableNumber = @event.TableNumber,
                                         Waiter = @event.Waiter,
                                     });
        }
    }
}
