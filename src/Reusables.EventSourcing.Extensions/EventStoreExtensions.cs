using System;
using System.Threading.Tasks;

namespace Reusables.EventSourcing.Extensions
{
    public static class EventStoreExtensions
    {
        public static void Act<TAggregate>(this IEventStore eventStore, Guid aggregateId, Action<TAggregate> action) where TAggregate : Aggregate
        {
            var aggregate = eventStore.GetById<TAggregate>(aggregateId);

            action(aggregate);

            eventStore.Save(aggregate);
        }

        public static async Task ActAsync<TAggregate>(this IEventStore eventStore, Guid aggregateId, Action<TAggregate> action) where TAggregate : Aggregate
        {
            var aggregate = await eventStore.GetByIdAsync<TAggregate>(aggregateId);

            action(aggregate);

            await eventStore.SaveAsync(aggregate);
        }
    }
}
