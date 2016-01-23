using System;
using Reusables.Diagnostics.Contracts;

namespace Reusables.BuildingBlocks
{
    public static class HandlerExtensions
    {
        public static IHandler<TMessage, TResponse> FollowedBy<TMessage, TTemp, TResponse>(this IHandler<TMessage, TTemp> firstHandler, IHandler<TTemp, TResponse> secondHandler)
        {
            return new SequentialHandler<TMessage, TTemp, TResponse>(firstHandler, secondHandler);
        }

        public static IHandler<TMessage, TResponse> FollowedBy<TMessage, TTemp, TResponse>(this IHandler<TMessage, TTemp> firstHandler, Func<TTemp, TResponse> func)
        {
            return new SequentialHandler<TMessage, TTemp, TResponse>(firstHandler, new AnonymousFuncHandler<TTemp, TResponse>(func));
        }

        public static IHandler<TMessage, bool> FollowedBy<TMessage, TTemp>(this IHandler<TMessage, TTemp> firstHandler, Predicate<TTemp> predicate)
        {
            return new SequentialHandler<TMessage, TTemp, bool>(firstHandler, new AnonymousPredicateHandler<TTemp>(predicate));
        }

        public static IHandler<TMessage, Nothing> FollowedBy<TMessage, TTemp>(this IHandler<TMessage, TTemp> firstHandler, Action<TTemp> action)
        {
            return new SequentialHandler<TMessage, TTemp, Nothing>(firstHandler, new AnonymousActionHandler<TTemp>(action));
        }

        private class SequentialHandler<TMessage, TTemp, TResponse> : IHandler<TMessage, TResponse>
        {
            private readonly IHandler<TMessage, TTemp> _firstHandler;
            private readonly IHandler<TTemp, TResponse> _secondHandler;

            public SequentialHandler(IHandler<TMessage, TTemp> firstHandler, IHandler<TTemp, TResponse> secondHandler)
            {
                _firstHandler = firstHandler;
                _secondHandler = secondHandler;
            }

            public TResponse Handle(TMessage message)
            {
                var temp = _firstHandler.Handle(message);

                return _secondHandler.Handle(temp);
            }
        }

        private class AnonymousFuncHandler<TMessage, TResponse> : IHandler<TMessage, TResponse>
        {
            private readonly Func<TMessage, TResponse> _func;

            public AnonymousFuncHandler(Func<TMessage, TResponse> func)
            {
                Requires.IsNotNull(func, nameof(func));

                _func = func;
            }

            public TResponse Handle(TMessage message)
            {
                return _func(message);
            }
        }

        private class AnonymousPredicateHandler<TMessage> : IHandler<TMessage, bool>
        {
            private readonly Predicate<TMessage> _predicate;

            public AnonymousPredicateHandler(Predicate<TMessage> predicate)
            {
                Requires.IsNotNull(predicate, nameof(predicate));

                _predicate = predicate;
            }

            public bool Handle(TMessage message)
            {
                return _predicate(message);
            }
        }

        private class AnonymousActionHandler<TMessage> : IHandler<TMessage, Nothing>
        {
            private readonly Action<TMessage> _action;

            public AnonymousActionHandler(Action<TMessage> action)
            {
                Requires.IsNotNull(action, nameof(action));

                _action = action;
            }

            public Nothing Handle(TMessage message)
            {
                _action(message);

                return new Nothing();
            }
        }
    }
}
