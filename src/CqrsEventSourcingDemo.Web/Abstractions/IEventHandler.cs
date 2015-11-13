using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions
{
    public interface IEventHandler
    {
        void Handle(object @event);
    }

    public interface IEventHandler<TEvent> : IEventHandler where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
