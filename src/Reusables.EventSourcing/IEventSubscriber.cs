namespace Reusables.EventSourcing
{
    public interface IEventSubscriber<TEvent>
    {
        void Handle(TEvent @event);
    }
}
