using System;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace CqrsEventSourcingDemo.Web.Abstractions.Decorators
{
    public class LoggingDecoratorCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public LoggingDecoratorCommandHandler(ILoggerFactory loggerFactory, ICommandHandler<TCommand> innerHandler)
        {
            _logger = loggerFactory.GetCurrentClassLogger();
            _innerHandler = innerHandler;
        }

        public void Handle(TCommand command)
        {
            try
            {
                _innerHandler.Handle(command);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Command: {0}", command.ToJson());
                throw;
            }
        }
    }
}
