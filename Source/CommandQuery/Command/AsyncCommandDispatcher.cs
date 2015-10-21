using System;
using System.Threading.Tasks;

namespace CommandQuery.Command
{
    public class AsyncCommandDispatcher : IAsyncCommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public AsyncCommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : Command
        {
            var handlerType = typeof (IAsyncCommandHandler<>).MakeGenericType(typeof (TCommand));

            var handler = (IAsyncCommandHandler<TCommand>) _serviceProvider.GetService(handlerType);

            await handler.HandleAsync(command);
        }
    }
}