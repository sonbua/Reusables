namespace CommandQuery.Core.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(Query<TResult> query);
    }
}