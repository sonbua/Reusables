using System.Threading.Tasks;

namespace CommandQuery.Query
{
    public interface IAsyncQueryHandler<TQuery, TResult> : IQueryHandler
    {
        Task<TResult> Handle(TQuery query);
    }
}