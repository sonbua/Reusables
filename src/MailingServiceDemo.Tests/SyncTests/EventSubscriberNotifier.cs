using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class EventSubscriberNotifier<TEvent> : IEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IEventSubscriber<TEvent> _innerSubscriber;

        public EventSubscriberNotifier(ILogger logger, IEventSubscriber<TEvent> innerSubscriber)
        {
            _logger = logger;
            _innerSubscriber = innerSubscriber;
        }

        public void Handle(TEvent @event)
        {
            _logger.Info($"{typeof (TEvent).Name}  ====>  {_innerSubscriber.GetType().Name}: {@event.ToJson()}");

            _innerSubscriber.Handle(@event);
        }
    }
}