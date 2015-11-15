using System;
using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IRequestDispatcher"/> interface.
    /// </summary>
    public class RequestDispatcher : IRequestDispatcher, ICommandDispatcher, IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void DispatchCommand<TCommand>(TCommand command)
        {
            var handler = (ICommandHandler<TCommand>) _serviceProvider.GetService(typeof (ICommandHandler<TCommand>));

            handler.Handle(command);
        }

        public async Task DispatchCommandAsync<TAsyncCommand>(TAsyncCommand command)
        {
            var handler = (IAsyncCommandHandler<TAsyncCommand>) _serviceProvider.GetService(typeof (IAsyncCommandHandler<TAsyncCommand>));

            await handler.HandleAsync(command).ConfigureAwait(false);
        }

        public TResult DispatchQuery<TResult>(Query<TResult> query)
        {
            var handlerType = typeof (IQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            dynamic handler = _serviceProvider.GetService(handlerType);

            return handler.Handle((dynamic) query);
        }

        public async Task<TResult> DispatchQueryAsync<TResult>(AsyncQuery<TResult> query)
        {
            var handlerType = typeof (IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            dynamic handler = _serviceProvider.GetService(handlerType);

            return await handler.HandleAsync((dynamic) query).ConfigureAwait(false);
        }
    }
}
