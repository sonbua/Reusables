namespace Reusables.EventSourcing
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
    }
}
