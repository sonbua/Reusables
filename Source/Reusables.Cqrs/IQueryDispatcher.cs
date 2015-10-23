using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(Query<TResult> query);

        Task<TResult> DispatchAsync<TResult>(AsyncQuery<TResult> query);
    }
}
