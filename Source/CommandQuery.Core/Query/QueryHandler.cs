namespace CommandQuery.Core.Query
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        public abstract TResult Handle(TQuery query);

        public object Handle(object query)
        {
            return Handle((TQuery) query);
        }
    }
}