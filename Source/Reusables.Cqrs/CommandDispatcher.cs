using System;
using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<TCommand>(TCommand command) where TCommand : Command
        {
            var handler = (ICommandHandler<TCommand>) _serviceProvider.GetService(typeof (ICommandHandler<TCommand>));

            handler.Handle(command);
        }

        public async Task DispatchAsync<TAsyncCommand>(TAsyncCommand command) where TAsyncCommand : AsyncCommand
        {
            var handler = (IAsyncCommandHandler<TAsyncCommand>) _serviceProvider.GetService(typeof (IAsyncCommandHandler<TAsyncCommand>));

            await handler.HandleAsync(command);
        }
    }
}
