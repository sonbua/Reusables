using System.Threading.Tasks;
using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class AsyncCommandHandlerReporter<TCommand> : IAsyncCommandHandler<TCommand>
    {
        private readonly IAsyncCommandHandler<TCommand> _innerHandler;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public AsyncCommandHandlerReporter(IAsyncCommandHandler<TCommand> innerHandler, IDbContext dbContext, ILogger logger)
        {
            _innerHandler = innerHandler;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task HandleAsync(TCommand command)
        {
            await _innerHandler.HandleAsync(command);

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