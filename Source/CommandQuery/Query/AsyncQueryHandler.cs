using System.Threading.Tasks;

namespace CommandQuery.Query
{
    public abstract class AsyncQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, Task<TResult>> where TQuery : Query<Task<TResult>>
    {
        public abstract Task<TResult> Handle(TQuery query);

        public object Handle(object query)
        {
            return Handle((TQuery) query);
        }
    }
}