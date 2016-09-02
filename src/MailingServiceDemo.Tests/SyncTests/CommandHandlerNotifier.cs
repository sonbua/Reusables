using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class CommandHandlerNotifier<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public CommandHandlerNotifier(ILogger logger, ICommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public void Handle(TCommand command)
        {
            _logger.Info($"{command.GetType().Name}  ====>  {_innerHandler.GetType().Name}: {command.ToJson()}");

            _innerHandler.Handle(command);
        }
    }
}