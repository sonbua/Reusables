using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    /// <summary>
    /// Provides a basic, non-generic implementation for <see cref="IQueryHandler{TResult}"/> interface.
    /// You are encouraged to inherit from this class, and override the generic version.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    /// <typeparam name="TResult">The type of query result.</typeparam>
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        public TResult Handle(object query)
        {
            return Handle((TQuery) query);
        }

        public abstract TResult Handle(TQuery query);
    }

    /// <summary>
    /// Provides a basic, non-generic implementation for <see cref="IAsyncQueryHandler{TResult}"/> interface.
    /// You are encouraged to inherit from this class, and override the generic version.
    /// </summary>
    /// <typeparam name="TAsyncQuery">The type of asynchronous query.</typeparam>
    /// <typeparam name="TResult">The type of asynchronous query result.</typeparam>
    public abstract class AsyncQueryHandler<TAsyncQuery, TResult> : IAsyncQueryHandler<TAsyncQuery, TResult> where TAsyncQuery : AsyncQuery<TResult>
    {
        public async Task<TResult> HandleAsync(object query)
        {
            return await HandleAsync((TAsyncQuery) query).ConfigureAwait(false);
        }

        public abstract Task<TResult> HandleAsync(TAsyncQuery query);
    }
}
