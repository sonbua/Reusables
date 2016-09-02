using System.Threading.Tasks;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class AsyncEventSubscriberNotifier<TEvent> : IAsyncEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IAsyncEventSubscriber<TEvent> _innerEventSubscriber;

        public AsyncEventSubscriberNotifier(ILogger logger, IAsyncEventSubscriber<TEvent> innerEventSubscriber)
        {
            _logger = logger;
            _innerEventSubscriber = innerEventSubscriber;
        }

        public async Task HandleAsync(TEvent @event)
        {
            _logger.Info($"{typeof (TEvent).Name}  ====>  {_innerEventSubscriber.GetType().Name}: {@event.ToJson()}");

            await _innerEventSubscriber.HandleAsync(@event);
        }
    }
}