using System;
using Reusables.Diagnostics.Contracts;

namespace Reusables.BuildingBlocks
{
    public static class HandlerExtensions
    {
        public static IHandler<TInput, TOutput> FollowedBy<TInput, TTemp, TOutput>(this IHandler<TInput, TTemp> firstHandler, IHandler<TTemp, TOutput> secondHandler)
        {
            return new SequentialHandler<TInput, TTemp, TOutput>(firstHandler, secondHandler);
        }

        public static IHandler<TInput, TOutput> FollowedBy<TInput, TTemp, TOutput>(this IHandler<TInput, TTemp> firstHandler, Func<TTemp, TOutput> func)
        {
            return new SequentialHandler<TInput, TTemp, TOutput>(firstHandler, new AnonymousFuncHandler<TTemp, TOutput>(func));
        }

        public static IHandler<TInput, bool> FollowedBy<TInput, TTemp>(this IHandler<TInput, TTemp> firstHandler, Predicate<TTemp> predicate)
        {
            return new SequentialHandler<TInput, TTemp, bool>(firstHandler, new AnonymousPredicateHandler<TTemp>(predicate));
        }

        public static IHandler<TInput, Nothing> FollowedBy<TInput, TTemp>(this IHandler<TInput, TTemp> firstHandler, Action<TTemp> action)
        {
            return new SequentialHandler<TInput, TTemp, Nothing>(firstHandler, new AnonymousActionHandler<TTemp>(action));
        }

        private class SequentialHandler<TInput, TTemp, TOutput> : IHandler<TInput, TOutput>
        {
            private readonly IHandler<TInput, TTemp> _firstHandler;
            private readonly IHandler<TTemp, TOutput> _secondHandler;

            public SequentialHandler(IHandler<TInput, TTemp> firstHandler, IHandler<TTemp, TOutput> secondHandler)
            {
                _firstHandler = firstHandler;
                _secondHandler = secondHandler;
            }

            public TOutput Handle(TInput input)
            {
                var temp = _firstHandler.Handle(input);

                return _secondHandler.Handle(temp);
            }
        }

        private class AnonymousFuncHandler<TInput, TOutput> : IHandler<TInput, TOutput>
        {
            private readonly Func<TInput, TOutput> _func;

            public AnonymousFuncHandler(Func<TInput, TOutput> func)
            {
                Requires.IsNotNull(func, nameof(func));

                _func = func;
            }

            public TOutput Handle(TInput input)
            {
                return _func(input);
            }
        }

        private class AnonymousPredicateHandler<TInput> : IHandler<TInput, bool>
        {
            private readonly Predicate<TInput> _predicate;

            public AnonymousPredicateHandler(Predicate<TInput> predicate)
            {
                Requires.IsNotNull(predicate, nameof(predicate));

                _predicate = predicate;
            }

            public bool Handle(TInput input)
            {
                return _predicate(input);
            }
        }

        private class AnonymousActionHandler<TInput> : IHandler<TInput, Nothing>
        {
            private readonly Action<TInput> _action;

            public AnonymousActionHandler(Action<TInput> action)
            {
                Requires.IsNotNull(action, nameof(action));

                _action = action;
            }

            public Nothing Handle(TInput input)
            {
                _action(input);

                return new Nothing();
            }
        }
    }
}
