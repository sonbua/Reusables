using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    /// <summary>
    /// Defines an interface for a query dispatcher. This dispatcher is responsible for distributing a query object to the right query handler, letting it process the query, then returning result back to the caller.
    /// </summary>
    public interface IQueryDispatcher
    {
        TResult DispatchQuery<TResult>(Query<TResult> query);

        Task<TResult> DispatchQueryAsync<TResult>(Query<TResult> query);
    }
}
