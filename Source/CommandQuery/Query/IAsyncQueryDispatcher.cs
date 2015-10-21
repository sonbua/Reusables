using System.Threading.Tasks;

namespace CommandQuery.Query
{
    public interface IAsyncQueryDispatcher
    {
        Task<TResult> Dispatch<TResult>(Query<TResult> query);
    }
}