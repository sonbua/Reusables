using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    /// <summary>
    /// Defines a common interface for synchronous query handlers.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    /// <typeparam name="TResult">The type of query result.</typeparam>
    public interface IQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        TResult Handle(TQuery query);
    }

    /// <summary>
    /// Defines a common interface for asynchronous query handlers.
    /// </summary>
    /// <typeparam name="TQuery">The type of asynchronous query.</typeparam>
    /// <typeparam name="TResult">The type of asynchronous query result.</typeparam>
    public interface IAsyncQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
