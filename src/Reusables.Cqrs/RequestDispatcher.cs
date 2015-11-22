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

            if (handler != null)
            {
                handler.Handle(command);
                return;
            }

            throw new NotSupportedException($"No handler found for this command: {typeof (TCommand)}");
        }

        public async Task DispatchCommandAsync<TCommand>(TCommand command)
        {
            var handler = (IAsyncCommandHandler<TCommand>) _serviceProvider.GetService(typeof (IAsyncCommandHandler<TCommand>));

            if (handler != null)
            {
                await handler.HandleAsync(command).ConfigureAwait(false);
                return;
            }

            throw new NotSupportedException($"No asynchronous handler found for this command: {typeof (TCommand)}");
        }

        public TResult DispatchQuery<TResult>(Query<TResult> query)
        {
            var queryType = query.GetType();
            var handlerType = typeof (IQueryHandler<,>).MakeGenericType(queryType, typeof (TResult));

            dynamic handler = _serviceProvider.GetService(handlerType);

            if (handler != null)
            {
                return handler.Handle((dynamic) query);
            }

            throw new NotSupportedException($"No handler found for this query: {queryType}");
        }

        public async Task<TResult> DispatchQueryAsync<TResult>(Query<TResult> query)
        {
            var queryType = query.GetType();
            var handlerType = typeof (IAsyncQueryHandler<,>).MakeGenericType(queryType, typeof (TResult));

            dynamic handler = _serviceProvider.GetService(handlerType);

            if (handler != null)
            {
                return await handler.HandleAsync((dynamic) query).ConfigureAwait(false);
            }

            throw new NotSupportedException($"No asynchronous handler found for this query: {queryType}");
        }
    }
}
