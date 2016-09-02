using System.Diagnostics;
using System.Threading.Tasks;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class AsyncCommandHandlerStopwatcher<TCommand> : IAsyncCommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly IAsyncCommandHandler<TCommand> _innerHandler;

        public AsyncCommandHandlerStopwatcher(ILogger logger, IAsyncCommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task HandleAsync(TCommand command)
        {
            var stopwatch = Stopwatch.StartNew();

            stopwatch.Start();

            await _innerHandler.HandleAsync(command);

            stopwatch.Stop();

            _logger.Info($"{typeof (TCommand).Name}: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}