using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class EventSubscriberDatabaseReporter<TEvent> : IEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;
        private readonly IEventSubscriber<TEvent> _innerSubscriber;

        public EventSubscriberDatabaseReporter(ILogger logger, IDbContext dbContext, IEventSubscriber<TEvent> innerSubscriber)
        {
            _logger = logger;
            _dbContext = dbContext;
            _innerSubscriber = innerSubscriber;
        }

        public void Handle(TEvent @event)
        {
            _innerSubscriber.Handle(@event);

            _logger.Info($">> {nameof(OutboxMessage)} table:");
            foreach (var message in _dbContext.Set<OutboxMessage>())
                _logger.Info($"   > {message.ToJson()}");

            _logger.Info($">> {nameof(OngoingMessage)} table:");
            foreach (var message in _dbContext.Set<OngoingMessage>())
                _logger.Info($"   > {message.ToJson()}");

            _logger.Info($">> {nameof(FaultMessage)} table:");
            foreach (var message in _dbContext.Set<FaultMessage>())
                _logger.Info($"   > {message.ToJson()}");

            _logger.Info($">> {nameof(SentMessage)} table:");
            foreach (var message in _dbContext.Set<SentMessage>())
                _logger.Info($"   > {message.ToJson()}");

            _logger.Info($">> {nameof(SuspiciousMessage)} table:");
            foreach (var message in _dbContext.Set<SuspiciousMessage>())
                _logger.Info($"   > {message.ToJson()}");
        }
    }
}