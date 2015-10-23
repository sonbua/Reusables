using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    public interface IQueryHandler<TResult>
    {
        TResult Handle(object query);
    }

    public interface IQueryHandler<TQuery, TResult> : IQueryHandler<TResult> where TQuery : Query<TResult>
    {
        TResult Handle(TQuery query);
    }

    public interface IAsyncQueryHandler<TResult>
    {
        Task<TResult> HandleAsync(object query);
    }

    public interface IAsyncQueryHandler<TAsyncQuery, TResult> : IAsyncQueryHandler<TResult> where TAsyncQuery : AsyncQuery<TResult>
    {
        Task<TResult> HandleAsync(TAsyncQuery query);
    }
}
