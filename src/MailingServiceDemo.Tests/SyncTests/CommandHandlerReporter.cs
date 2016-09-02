using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class CommandHandlerReporter<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _innerHandler;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public CommandHandlerReporter(ICommandHandler<TCommand> innerHandler, IDbContext dbContext, ILogger logger)
        {
            _innerHandler = innerHandler;
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Handle(TCommand command)
        {
            _innerHandler.Handle(command);

            _logger.Info($"Handled {typeof (TCommand).Name} command: {command.ToJson()}");

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