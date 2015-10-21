namespace CommandQuery.Query
{
    public interface IQueryHandler
    {
        object Handle(object query);
    }
}