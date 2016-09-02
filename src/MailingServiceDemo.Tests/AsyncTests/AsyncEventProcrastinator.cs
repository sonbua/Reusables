using System.Threading.Tasks;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class AsyncEventProcrastinator<TEvent> : IAsyncEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IAsyncEventSubscriber<TEvent> _innerEventSubscriber;

        public AsyncEventProcrastinator(ILogger logger, IAsyncEventSubscriber<TEvent> innerEventSubscriber)
        {
            _logger = logger;
            _innerEventSubscriber = innerEventSubscriber;
        }

        public async Task HandleAsync(TEvent @event)
        {
            var delay = Randomizer.Next(100);

            _logger.Info($"Delaying {delay} ms...");

            await Task.Delay(delay);

            await _innerEventSubscriber.HandleAsync(@event);
        }
    }
}