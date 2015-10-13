namespace CommandQuery.Core.Query
{
    public interface IQueryHandler
    {
        object Handle(object query);
    }

    public interface IQueryHandler<TQuery, TResult> : IQueryHandler where TQuery : Query<TResult>
    {
        TResult Handle(TQuery query);
    }
}