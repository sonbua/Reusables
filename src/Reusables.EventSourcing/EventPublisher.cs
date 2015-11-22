using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

                return;
            }

            var nullableListenerAttribute = @event.GetType().GetCustomAttribute<NullableListenerAttribute>();

            if (nullableListenerAttribute != null)
            {
                _logger.Info($"No subscriber found for this event: {@event.GetType()}");

                return;
            }

            throw new NotSupportedException($"No subscriber found for this event: {@event.GetType()}");
        }

        public async Task PublishAsync<TEvent>(TEvent @event)
        {
            var handlerType = typeof (IAsyncEventSubscriber<>).MakeGenericType(@event.GetType());

            var handlers = _serviceProvider.Invoke(handlerType).ToList();

            if (handlers.Any())
            {
                var tasks = new List<Task>();

                foreach (dynamic handler in handlers)
                {
                    tasks.Add(handler.HandleAsync((dynamic) @event));
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);

                return;
            }

            var nullableListenerAttribute = @event.GetType().GetCustomAttribute<NullableListenerAttribute>();

            if (nullableListenerAttribute != null)
            {
                _logger.Info($"No asynchronous subscriber found for this event: {@event.GetType()}");

                return;
            }

            throw new NotSupportedException($"No asynchronous subscriber found for this event: {@event.GetType()}");
        }
    }
}
