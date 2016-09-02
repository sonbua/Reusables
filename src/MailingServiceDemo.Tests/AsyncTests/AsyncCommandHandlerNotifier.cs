using System.Threading.Tasks;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class AsyncCommandHandlerNotifier<TCommand> : IAsyncCommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly IAsyncCommandHandler<TCommand> _innerHandler;

        public AsyncCommandHandlerNotifier(ILogger logger, IAsyncCommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task HandleAsync(TCommand command)
        {
            _logger.Info($"{command.GetType().Name}  ====>  {_innerHandler.GetType().Name}: {command.ToJson()}");

            await _innerHandler.HandleAsync(command);
        }
    }
}