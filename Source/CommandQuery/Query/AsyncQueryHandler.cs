using System.Threading.Tasks;

namespace CommandQuery.Query
{
    public abstract class AsyncQueryHandler<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult>
    {
        public abstract Task<TResult> Handle(TQuery query);

        public object Handle(object query)
        {
            return Handle((TQuery) query);
        }
    }
}