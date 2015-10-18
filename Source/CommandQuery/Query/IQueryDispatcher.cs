namespace CommandQuery.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(Query<TResult> query);
    }
}