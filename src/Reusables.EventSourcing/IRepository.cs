using System;
using System.Threading.Tasks;

namespace Reusables.EventSourcing
{
    public interface IRepository
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate, new();

        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : Aggregate, new();

        void Save(Aggregate aggregate);

        Task SaveAsync(Aggregate aggregate);
    }
}
