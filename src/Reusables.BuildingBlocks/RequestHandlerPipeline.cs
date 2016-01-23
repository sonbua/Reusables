using System.Collections.Generic;
using System.Linq;
using Reusables.Diagnostics.Contracts;

namespace Reusables.BuildingBlocks
{
    public class RequestHandlerPipeline<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;
        private readonly IEnumerable<IPreRequestHandler<TRequest>> _preRequestHandlers;
        private readonly IEnumerable<IPostRequestHandler<TRequest, TResponse>> _postRequestHandlers;

        public RequestHandlerPipeline(IRequestHandler<TRequest, TResponse> innerHandler,
                                      IEnumerable<IPreRequestHandler<TRequest>> preRequestHandlers,
                                      IEnumerable<IPostRequestHandler<TRequest, TResponse>> postRequestHandlers)
        {
            Requires.IsNotNull(innerHandler, nameof(innerHandler));
            Requires.IsNotNull(preRequestHandlers, nameof(preRequestHandlers));
            Requires.IsNotNull(postRequestHandlers, nameof(postRequestHandlers));

            _innerHandler = innerHandler;
            _preRequestHandlers = preRequestHandlers;
            _postRequestHandlers = postRequestHandlers;
        }

        public TResponse Handle(TRequest request)
        {
            foreach (var preRequestHandler in _preRequestHandlers)
            {
                preRequestHandler.Handle(request);
            }

            var result = _innerHandler.Handle(request);

            foreach (var postRequestHandler in _postRequestHandlers)
            {
                postRequestHandler.Handle(request, result);
            }

            return result;
        }
    }
}
