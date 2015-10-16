using System;

namespace CommandQuery.Core.Command
{
    public abstract class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        protected CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<TCommand>(TCommand command) where TCommand : Command
        {
            var handlerType = typeof (ICommandHandler<>).MakeGenericType(typeof (TCommand));

            var handler = (ICommandHandler) _serviceProvider.GetService(handlerType);

            handler.Handle(command);
        }
    }
}