using System;
using System.Collections.Generic;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions
{
    public class EventPublisher : IEventPublisher
    {
        private readonly Antlr.Runtime.Misc.Func<Type, IEnumerable<IEventHandler>> _serviceProvider;

        public EventPublisher(Antlr.Runtime.Misc.Func<Type, IEnumerable<IEventHandler>> serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var handlerType = typeof (IEventHandler<>).MakeGenericType(@event.GetType());

            var handlers = _serviceProvider.Invoke(handlerType);

            foreach (var handler in handlers)
            {
                handler.Handle(@event);
            }
        }
    }
}
