using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
    }
}
