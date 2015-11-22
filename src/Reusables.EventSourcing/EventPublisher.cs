using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reusables.Diagnostics.Logging;
using Reusables.Util.Extensions;

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

            if (handlers.IsNullOrEmpty())
            {
                _logger.Info($"No subscriber found for this event: {@event.GetType()}");

                return;
            }

            foreach (dynamic handler in handlers)
            {
                handler.Handle((dynamic) @event);
            }
        }

        public async Task PublishAsync<TEvent>(TEvent @event)
        {
            var handlerType = typeof (IAsyncEventSubscriber<>).MakeGenericType(@event.GetType());

            var handlers = _serviceProvider.Invoke(handlerType).ToList();

            if (handlers.IsNullOrEmpty())
            {
                _logger.Info($"No asynchronous subscriber found for this event: {@event.GetType()}");

                return;
            }

            var tasks = new List<Task>();

            foreach (dynamic handler in handlers)
            {
                tasks.Add(handler.HandleAsync((dynamic) @event));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
