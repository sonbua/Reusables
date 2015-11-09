using System;
using System.Threading.Tasks;

namespace Reusables.EventSourcing
{
    public interface IRepository
    {
        Task<TAggregate> GetById<TAggregate>(Guid id) where TAggregate : Aggregate;

        Task Save(Aggregate aggregate);
    }
}
