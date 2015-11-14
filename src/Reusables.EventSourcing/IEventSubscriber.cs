namespace Reusables.EventSourcing
{
    public interface IEventSubscriber<TEvent> where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
