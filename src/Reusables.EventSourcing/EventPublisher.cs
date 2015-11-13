using System;
using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    public class EventPublisher : IEventPublisher
    {
        private readonly Func<Type, IEnumerable<IEventSubscriber>> _serviceProvider;

        public EventPublisher(Func<Type, IEnumerable<IEventSubscriber>> serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var handlerType = typeof (IEventSubscriber<>).MakeGenericType(@event.GetType());

            var handlers = _serviceProvider.Invoke(handlerType);

            foreach (var handler in handlers)
            {
                handler.Handle(@event);
            }
        }
    }
}
