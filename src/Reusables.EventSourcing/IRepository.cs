using System;
using System.Threading.Tasks;

namespace Reusables.EventSourcing
{
    public interface IRepository
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : Aggregate;

        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : Aggregate;

        void Save(Aggregate aggregate);

        Task SaveAsync(Aggregate aggregate);
    }
}
