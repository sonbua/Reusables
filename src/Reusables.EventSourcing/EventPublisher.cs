using System;
using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    public class EventPublisher : IEventPublisher
    {
        private readonly Func<Type, IEnumerable<object>> _serviceProvider;

        public EventPublisher(Func<Type, IEnumerable<object>> serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            var handlerType = typeof (IEventSubscriber<>).MakeGenericType(@event.GetType());

            var handlers = _serviceProvider.Invoke(handlerType);

            foreach (dynamic handler in handlers)
            {
                handler.Handle((dynamic) @event);
            }
        }
    }
}
