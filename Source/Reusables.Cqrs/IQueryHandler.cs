using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    public interface IQueryHandler<TResult>
    {
        TResult Handle(object query);
    }

    /// <summary>
    /// Defines a common interface for synchronous query handlers.
    /// If you are to create derived types from this interface, consider inheriting from the <see cref="QueryHandler{TQuery,TResult}"/> base class instead.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    /// <typeparam name="TResult">The type of query result.</typeparam>
    public interface IQueryHandler<TQuery, TResult> : IQueryHandler<TResult> where TQuery : Query<TResult>
    {
        TResult Handle(TQuery query);
    }

    public interface IAsyncQueryHandler<TResult>
    {
        Task<TResult> HandleAsync(object query);
    }

    /// <summary>
    /// Defines a common interface for asynchronous query handlers.
    /// If you are to create derived type from this interface, consider inheriting from the <see cref="AsyncQueryHandler{TAsyncQuery,TResult}"/> base class instead.
    /// </summary>
    /// <typeparam name="TAsyncQuery">The type of asynchronous query.</typeparam>
    /// <typeparam name="TResult">The type of asynchronous query result.</typeparam>
    public interface IAsyncQueryHandler<TAsyncQuery, TResult> : IAsyncQueryHandler<TResult> where TAsyncQuery : AsyncQuery<TResult>
    {
        Task<TResult> HandleAsync(TAsyncQuery query);
    }
}
