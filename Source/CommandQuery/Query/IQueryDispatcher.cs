using System.Threading.Tasks;

namespace CommandQuery.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(Query<TResult> query);

        Task<TResult> DispatchAsync<TResult>(AsyncQuery<TResult> query);
    }
}