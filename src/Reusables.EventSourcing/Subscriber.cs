namespace Reusables.EventSourcing
{
    public abstract class Subscriber<TEvent> : IEventSubscriber<TEvent> where TEvent : Event
    {
        public void Handle(object @event)
        {
            Handle((TEvent) @event);
        }

        public abstract void Handle(TEvent @event);
    }
}
