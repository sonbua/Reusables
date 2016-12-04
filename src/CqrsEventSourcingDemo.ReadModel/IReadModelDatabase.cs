namespace CqrsEventSourcingDemo.ReadModel
{
    public interface IReadModelDatabase
    {
        IReadModelSet<TReadModel> Set<TReadModel>() where TReadModel : ReadModel;
    }
}