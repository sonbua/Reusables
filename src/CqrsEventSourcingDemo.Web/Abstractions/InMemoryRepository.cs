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
        private readonly IAggregateFactory _aggregateFactory;
        private readonly IEventPublisher _eventPublisher;

        public InMemoryRepository(IAggregateFactory aggregateFactory, IEventPublisher eventPublisher)
        {
            _aggregateFactory = aggregateFactory;
            _eventPublisher = eventPublisher;
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate
        {
            List<EventData> eventDataHistory;

            if (!_eventStorage.TryGetValue(id, out eventDataHistory))
            {
                return _aggregateFactory.Create<TAggregate>(new Event[0]);
            }

            var history = eventDataHistory.Select(x => x.FromEventData());

            return _aggregateFactory.Create<TAggregate>(history);
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

            foreach (var @event in events)
            {
                _eventPublisher.Publish(@event);
            }

            aggregate.ClearUncommittedEvents();
        }

        public Task SaveAsync(Aggregate aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
