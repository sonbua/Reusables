using System;
using System.Collections.Generic;
using System.Linq;
using Reusables.Diagnostics.Contracts;
using Reusables.Util.Extensions;

namespace Reusables.BuildingBlocks.Extensions
{
    public static class RequestHandlerExtensions
    {
        public static IRequestHandler<TRequest, TResponse> FollowedBy<TRequest, TTemp, TResponse>(this IRequestHandler<TRequest, TTemp> firstHandler, IRequestHandler<TTemp, TResponse> secondHandler)
        {
            Requires.IsNotNull(firstHandler, nameof(firstHandler));
            Requires.IsNotNull(secondHandler, nameof(secondHandler));

            return new SequentialRequestHandler<TRequest, TTemp, TResponse>(firstHandler, secondHandler);
        }

        public static IRequestHandler<TRequest, TResponse> FollowedBy<TRequest, TTemp, TResponse>(this IRequestHandler<TRequest, TTemp> firstHandler, Func<TTemp, TResponse> func)
        {
            Requires.IsNotNull(firstHandler, nameof(firstHandler));
            Requires.IsNotNull(func, nameof(func));

            return new SequentialRequestHandler<TRequest, TTemp, TResponse>(firstHandler, new AnonymousFuncHandler<TTemp, TResponse>(func));
        }

        public static IRequestHandler<TRequest, bool> FollowedBy<TRequest, TTemp>(this IRequestHandler<TRequest, TTemp> firstHandler, Predicate<TTemp> predicate)
        {
            Requires.IsNotNull(firstHandler, nameof(firstHandler));
            Requires.IsNotNull(predicate, nameof(predicate));

            return new SequentialRequestHandler<TRequest, TTemp, bool>(firstHandler, new AnonymousPredicateHandler<TTemp>(predicate));
        }

        public static IRequestHandler<TRequest, Nothing> FollowedBy<TRequest, TTemp>(this IRequestHandler<TRequest, TTemp> firstHandler, Action<TTemp> action)
        {
            Requires.IsNotNull(firstHandler, nameof(firstHandler));
            Requires.IsNotNull(action, nameof(action));

            return new SequentialRequestHandler<TRequest, TTemp, Nothing>(firstHandler, new AnonymousActionHandler<TTemp>(action));
        }

        public static IRequestHandler<TRequest, IEnumerable<TResult>> Where<TRequest, TResult>(this IRequestHandler<TRequest, IEnumerable<TResult>> handler, Func<TResult, bool> predicate)
        {
            Requires.IsNotNull(handler, nameof(handler));
            Requires.IsNotNull(predicate, nameof(predicate));

            return handler.FollowedBy(source => source.Where(predicate));
        }

        public static IRequestHandler<TRequest, IEnumerable<TResult>> Select<TRequest, TSource, TResult>(this IRequestHandler<TRequest, IEnumerable<TSource>> handler, Func<TSource, TResult> selector)
        {
            Requires.IsNotNull(handler, nameof(handler));
            Requires.IsNotNull(selector, nameof(selector));

            return handler.FollowedBy(source => source.Select(selector));
        }

        public static IRequestHandler<TRequest, int> Count<TRequest, TSource>(this IRequestHandler<TRequest, IEnumerable<TSource>> handler)
        {
            Requires.IsNotNull(handler, nameof(handler));

            return handler.FollowedBy(source => source.Count());
        }
        public static IRequestHandler<TRequest, int> Count<TRequest, TSource>(this IRequestHandler<TRequest, IEnumerable<TSource>> handler, Func<TSource, bool> predicate)
        {
            Requires.IsNotNull(handler, nameof(handler));
            Requires.IsNotNull(predicate, nameof(predicate));

            return handler.FollowedBy(source => source.Count(predicate));
        }

        public static IRequestHandler<TRequest, Nothing> ForEach<TRequest, T>(this IRequestHandler<TRequest, IEnumerable<T>> handler, Action<T> action)
        {
            Requires.IsNotNull(handler, nameof(handler));
            Requires.IsNotNull(action, nameof(action));

            return handler.FollowedBy(source => source.ForEach(action));
        }

        private class SequentialRequestHandler<TRequest, TTemp, TResponse> : IRequestHandler<TRequest, TResponse>
        {
            private readonly IRequestHandler<TRequest, TTemp> _firstHandler;
            private readonly IRequestHandler<TTemp, TResponse> _secondHandler;

            public SequentialRequestHandler(IRequestHandler<TRequest, TTemp> firstHandler, IRequestHandler<TTemp, TResponse> secondHandler)
            {
                _firstHandler = firstHandler;
                _secondHandler = secondHandler;
            }

            public TResponse Handle(TRequest request)
            {
                var temp = _firstHandler.Handle(request);

                return _secondHandler.Handle(temp);
            }
        }
    }

    internal class AnonymousFuncHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        private readonly Func<TRequest, TResponse> _func;

        public AnonymousFuncHandler(Func<TRequest, TResponse> func)
        {
            _func = func;
        }

        public TResponse Handle(TRequest request)
        {
            return _func(request);
        }
    }

    internal class AnonymousPredicateHandler<TRequest> : IRequestHandler<TRequest, bool>
    {
        private readonly Predicate<TRequest> _predicate;

        public AnonymousPredicateHandler(Predicate<TRequest> predicate)
        {
            _predicate = predicate;
        }

        public bool Handle(TRequest request)
        {
            return _predicate(request);
        }
    }

    internal class AnonymousActionHandler<TRequest> : IRequestHandler<TRequest, Nothing>
    {
        private readonly Action<TRequest> _action;

        public AnonymousActionHandler(Action<TRequest> action)
        {
            _action = action;
        }

        public Nothing Handle(TRequest request)
        {
            _action(request);

            return new Nothing();
        }
    }
}
