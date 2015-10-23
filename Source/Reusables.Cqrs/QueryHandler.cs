using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        public TResult Handle(object query)
        {
            return Handle((TQuery) query);
        }

        public abstract TResult Handle(TQuery query);
    }

    public abstract class AsyncQueryHandler<TAsyncQuery, TResult> : IAsyncQueryHandler<TAsyncQuery, TResult> where TAsyncQuery : AsyncQuery<TResult>
    {
        public async Task<TResult> HandleAsync(object query)
        {
            return await HandleAsync((TAsyncQuery) query);
        }

        public abstract Task<TResult> HandleAsync(TAsyncQuery query);
    }
}