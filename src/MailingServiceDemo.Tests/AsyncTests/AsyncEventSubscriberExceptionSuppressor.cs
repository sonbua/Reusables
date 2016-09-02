using System;
using System.Threading.Tasks;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class AsyncEventSubscriberExceptionSuppressor<TEvent> : IAsyncEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IAsyncEventSubscriber<TEvent> _innerSubscriber;

        public AsyncEventSubscriberExceptionSuppressor(ILogger logger, IAsyncEventSubscriber<TEvent> innerSubscriber)
        {
            _logger = logger;
            _innerSubscriber = innerSubscriber;
        }

        public async Task HandleAsync(TEvent @event)
        {
            try
            {
                await _innerSubscriber.HandleAsync(@event);
            }
            catch (Exception exception)
            {
                _logger.Error($"{@event.GetType().Name}  ====>  {_innerSubscriber.GetType().Name} - {exception}");
            }
        }
    }
}