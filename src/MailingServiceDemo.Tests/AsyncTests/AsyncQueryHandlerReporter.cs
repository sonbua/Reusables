using System;
using System.Threading.Tasks;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class AsyncQueryHandlerReporter<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        private readonly ILogger _logger;
        private readonly IAsyncQueryHandler<TQuery, TResult> _innerHandler;

        public AsyncQueryHandlerReporter(ILogger logger, IAsyncQueryHandler<TQuery, TResult> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            var result = await _innerHandler.HandleAsync(query).ConfigureAwait(false);

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