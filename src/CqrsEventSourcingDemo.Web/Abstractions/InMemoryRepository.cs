using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsEventSourcingDemo.Web.Controllers;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;
using Reusables.Util.Extensions;

namespace CqrsEventSourcingDemo.Web.Abstractions
{
    public class InMemoryRepository : IRepository
    {
        private static readonly Dictionary<Guid, List<EventData>> _eventStorage = new Dictionary<Guid, List<EventData>>();

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate
        {
            List<EventData> listOfEventData;

            if (_eventStorage.TryGetValue(id, out listOfEventData))
            {
                var events = listOfEventData.Select(x => x.FromEventData());

                return (TAggregate) (Aggregate) new ClassAggregate(events);
            }

            return (TAggregate) (Aggregate) new ClassAggregate(Enumerable.Empty<object>());
        }

        public Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : Aggregate
        {
            throw new NotImplementedException();
        }

        public void Save(Aggregate aggregate)
        {
            var events = aggregate.GetUncommittedEvents();

            if (events.IsNullOrEmpty())
            {
                return;
            }

            var aggregateType = aggregate.GetType().Name;
            var originalVersion = aggregate.Version - events.LongLength + 1;
            var eventsToSave = events.Select(e => e.ToEventData(aggregateType, aggregate.Id, originalVersion++))
                                     .ToArray();

            List<EventData> existingEvents;

            if (_eventStorage.TryGetValue(aggregate.Id, out existingEvents))
            {
                existingEvents.AddRange(eventsToSave);
            }
            else
            {
                _eventStorage.Add(aggregate.Id, eventsToSave.ToList());
            }

            aggregate.ClearUncommittedEvents();
        }

        public Task SaveAsync(Aggregate aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
