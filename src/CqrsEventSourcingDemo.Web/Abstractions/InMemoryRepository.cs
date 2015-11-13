using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;
using Reusables.Util.Extensions;

namespace CqrsEventSourcingDemo.Web.Abstractions
{
    public class InMemoryRepository : IRepository
    {
        private static readonly Dictionary<Guid, List<EventData>> _eventStorage = new Dictionary<Guid, List<EventData>>();

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate, new()
        {
            List<EventData> eventDataHistory;

            if (!_eventStorage.TryGetValue(id, out eventDataHistory))
            {
                return new TAggregate();
            }

            var history = eventDataHistory.Select(x => x.FromEventData());

            return new TAggregate().Replay(history);
        }

        public Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : Aggregate, new()
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
                                     .ToList();

            List<EventData> existingEvents;

            if (_eventStorage.TryGetValue(aggregate.Id, out existingEvents))
            {
                existingEvents.AddRange(eventsToSave);
            }
            else
            {
                _eventStorage.Add(aggregate.Id, eventsToSave);
            }

            aggregate.ClearUncommittedEvents();
        }

        public Task SaveAsync(Aggregate aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
