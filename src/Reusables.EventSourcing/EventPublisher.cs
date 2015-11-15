using System;
using System.Collections.Generic;
using System.Linq;
using Reusables.Diagnostics.Logging;

namespace Reusables.EventSourcing
{
    public class EventPublisher : IEventPublisher
    {
        private readonly Func<Type, IEnumerable<object>> _serviceProvider;
        private readonly ILogger _logger;

        public EventPublisher(Func<Type, IEnumerable<object>> serviceProvider, ILogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            var handlerType = typeof (IEventSubscriber<>).MakeGenericType(@event.GetType());

            var handlers = _serviceProvider.Invoke(handlerType).ToList();

            if (handlers.Any())
            {
                foreach (dynamic handler in handlers)
                {
                    handler.Handle((dynamic) @event);
                }
            }

            _logger.Warn($"No subscriber found for this event: {@event.GetType()}");
        }
    }
}
