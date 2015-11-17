using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reusables.Diagnostics.Logging;
using Reusables.Util.Extensions;

namespace Reusables.EventSourcing
{
    public class AsyncEventPublisher : IAsyncEventPublisher
    {
        private readonly Func<Type, IEnumerable<object>> _serviceProvider;
        private readonly ILogger _logger;

        public AsyncEventPublisher(Func<Type, IEnumerable<object>> serviceProvider, ILogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
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
                tasks.Add(handler.Handle((dynamic) @event));
            }

            await Task.WhenAll(tasks);
        }
    }
}
