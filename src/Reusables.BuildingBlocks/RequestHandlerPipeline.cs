using Reusables.Diagnostics.Contracts;

namespace Reusables.BuildingBlocks
{
    public class RequestHandlerPipeline<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;
        private readonly IPreRequestHandler<TRequest> _preRequestHandler;
        private readonly IPostRequestHandler<TRequest, TResponse> _postRequestHandler;

        public RequestHandlerPipeline(IRequestHandler<TRequest, TResponse> innerHandler,
                                      IPreRequestHandler<TRequest> preRequestHandler,
                                      IPostRequestHandler<TRequest, TResponse> postRequestHandler)
        {
            Requires.IsNotNull(innerHandler, nameof(innerHandler));
            Requires.IsNotNull(preRequestHandler, nameof(preRequestHandler));
            Requires.IsNotNull(postRequestHandler, nameof(postRequestHandler));

            _innerHandler = innerHandler;
            _preRequestHandler = preRequestHandler;
            _postRequestHandler = postRequestHandler;
        }

        public TResponse Handle(TRequest request)
        {
            _preRequestHandler.Handle(request);

            var response = _innerHandler.Handle(request);

            _postRequestHandler.Handle(request, response);

            return response;
        }
    }
}
