using System;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class QueryHandlerReporter<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        private readonly ILogger _logger;
        private readonly IQueryHandler<TQuery, TResult> _innerHandler;

        public QueryHandlerReporter(ILogger logger, IQueryHandler<TQuery, TResult> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public TResult Handle(TQuery query)
        {
            var result = _innerHandler.Handle(query);

            string resultString;

            try
            {
                resultString = result.ToJson();
            }
            catch (Exception)
            {
                resultString = "{}";
            }

            _logger.Info($"{query.GetType().Name}  ====>  {_innerHandler.GetType().Name}: {query.ToJson()} => {resultString}");

            return result;
        }
    }
}