using System;

namespace CommandQuery.Query
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

            var handler = (IQueryHandler) _serviceProvider.GetService(handlerType);

            return (TResult) handler.Handle(query);
        }
    }
}