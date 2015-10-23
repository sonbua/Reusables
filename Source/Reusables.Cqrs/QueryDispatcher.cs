using System;
using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Dispatch<TResult>(Query<TResult> query)
        {
            var handlerType = typeof (IQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            var handler = (IQueryHandler<TResult>) _serviceProvider.GetService(handlerType);

            return handler.Handle(query);
        }

        public async Task<TResult> DispatchAsync<TResult>(AsyncQuery<TResult> query)
        {
            var handlerType = typeof (IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            var handler = (IAsyncQueryHandler<TResult>) _serviceProvider.GetService(handlerType);

            return await handler.HandleAsync(query);
        }
    }
}
