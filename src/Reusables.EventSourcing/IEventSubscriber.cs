namespace Reusables.EventSourcing
{
    public interface IEventSubscriber
    {
        void Handle(object @event);
    }

    public interface IEventSubscriber<TEvent> : IEventSubscriber where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
