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

        void ICommandDispatcher.Dispatch<TCommand>(TCommand command)
        {
            var handler = (ICommandHandler<TCommand>) _serviceProvider.GetService(typeof (ICommandHandler<TCommand>));

            handler.Handle(command);
        }

        async Task ICommandDispatcher.DispatchAsync<TAsyncCommand>(TAsyncCommand command)
        {
            var handler = (IAsyncCommandHandler<TAsyncCommand>) _serviceProvider.GetService(typeof (IAsyncCommandHandler<TAsyncCommand>));

            await handler.HandleAsync(command).ConfigureAwait(false);
        }

        TResult IQueryDispatcher.Dispatch<TResult>(Query<TResult> query)
        {
            var handlerType = typeof (IQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            var handler = (IQueryHandler<TResult>) _serviceProvider.GetService(handlerType);

            return handler.Handle(query);
        }

        async Task<TResult> IQueryDispatcher.DispatchAsync<TResult>(AsyncQuery<TResult> query)
        {
            var handlerType = typeof (IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            var handler = (IAsyncQueryHandler<TResult>) _serviceProvider.GetService(handlerType);

            return await handler.HandleAsync(query).ConfigureAwait(false);
        }
    }
}
