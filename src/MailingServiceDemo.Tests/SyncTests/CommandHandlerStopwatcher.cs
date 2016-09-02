using System.Diagnostics;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class CommandHandlerStopwatcher<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public CommandHandlerStopwatcher(ILogger logger, ICommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public void Handle(TCommand command)
        {
            var stopwatch = Stopwatch.StartNew();

            stopwatch.Start();

            _innerHandler.Handle(command);

            stopwatch.Stop();

            _logger.Info($"{typeof (TCommand).Name}: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}