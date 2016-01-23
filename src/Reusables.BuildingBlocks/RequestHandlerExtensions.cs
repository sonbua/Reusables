using System;
using Reusables.Diagnostics.Contracts;

namespace Reusables.BuildingBlocks
{
    public static class RequestHandlerExtensions
    {
        public static IRequestHandler<TRequest, TResponse> FollowedBy<TRequest, TTemp, TResponse>(this IRequestHandler<TRequest, TTemp> firstHandler, IRequestHandler<TTemp, TResponse> secondHandler)
        {
            return new SequentialRequestHandler<TRequest, TTemp, TResponse>(firstHandler, secondHandler);
        }

        public static IRequestHandler<TRequest, TResponse> FollowedBy<TRequest, TTemp, TResponse>(this IRequestHandler<TRequest, TTemp> firstHandler, Func<TTemp, TResponse> func)
        {
            return new SequentialRequestHandler<TRequest, TTemp, TResponse>(firstHandler, new AnonymousFuncHandler<TTemp, TResponse>(func));
        }

        public static IRequestHandler<TRequest, bool> FollowedBy<TRequest, TTemp>(this IRequestHandler<TRequest, TTemp> firstHandler, Predicate<TTemp> predicate)
        {
            return new SequentialRequestHandler<TRequest, TTemp, bool>(firstHandler, new AnonymousPredicateHandler<TTemp>(predicate));
        }

        public static IRequestHandler<TRequest, Nothing> FollowedBy<TRequest, TTemp>(this IRequestHandler<TRequest, TTemp> firstHandler, Action<TTemp> action)
        {
            return new SequentialRequestHandler<TRequest, TTemp, Nothing>(firstHandler, new AnonymousActionHandler<TTemp>(action));
        }

        private class SequentialRequestHandler<TRequest, TTemp, TResponse> : IRequestHandler<TRequest, TResponse>
        {
            private readonly IRequestHandler<TRequest, TTemp> _firstHandler;
            private readonly IRequestHandler<TTemp, TResponse> _secondHandler;

            public SequentialRequestHandler(IRequestHandler<TRequest, TTemp> firstHandler, IRequestHandler<TTemp, TResponse> secondHandler)
            {
                Requires.IsNotNull(firstHandler, nameof(firstHandler));
                Requires.IsNotNull(secondHandler, nameof(secondHandler));

                _firstHandler = firstHandler;
                _secondHandler = secondHandler;
            }

            public TResponse Handle(TRequest request)
            {
                var temp = _firstHandler.Handle(request);

                return _secondHandler.Handle(temp);
            }
        }

        private class AnonymousFuncHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        {
            private readonly Func<TRequest, TResponse> _func;

            public AnonymousFuncHandler(Func<TRequest, TResponse> func)
            {
                Requires.IsNotNull(func, nameof(func));

                _func = func;
            }

            public TResponse Handle(TRequest request)
            {
                return _func(request);
            }
        }

        private class AnonymousPredicateHandler<TRequest> : IRequestHandler<TRequest, bool>
        {
            private readonly Predicate<TRequest> _predicate;

            public AnonymousPredicateHandler(Predicate<TRequest> predicate)
            {
                Requires.IsNotNull(predicate, nameof(predicate));

                _predicate = predicate;
            }

            public bool Handle(TRequest request)
            {
                return _predicate(request);
            }
        }

        private class AnonymousActionHandler<TRequest> : IRequestHandler<TRequest, Nothing>
        {
            private readonly Action<TRequest> _action;

            public AnonymousActionHandler(Action<TRequest> action)
            {
                Requires.IsNotNull(action, nameof(action));

                _action = action;
            }

            public Nothing Handle(TRequest request)
            {
                _action(request);

                return new Nothing();
            }
        }
    }
}
