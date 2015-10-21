using System;
using System.Threading.Tasks;

namespace CommandQuery.Query
{
    public class AsyncQueryDispatcher : IAsyncQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public AsyncQueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> Dispatch<TResult>(Query<TResult> query)
        {
            var handlerType = typeof (IAsyncQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            var handler = (IQueryHandler) _serviceProvider.GetService(handlerType);

            return await (Task<TResult>) handler.Handle(query);
        }
    }
}