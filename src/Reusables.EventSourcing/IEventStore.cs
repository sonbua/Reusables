using System;
using System.Threading.Tasks;

namespace Reusables.EventSourcing
{
    public interface IEventStore
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate;

        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : Aggregate;

        void Save<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate;

        Task SaveAsync<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate;
    }
}
