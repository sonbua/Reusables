using System;
using Reusables.Diagnostics.Contracts;
using Reusables.Util.Extensions;

namespace Reusables.BuildingBlocks.Extensions
{
    public static class DelegateExtensions
    {
        public static IRequestHandler<TRequest, TResponse> ToRequestHandler<TRequest, TResponse>(this Func<TRequest, TResponse> func)
        {
            Requires.IsNotNull(func, nameof(func));

            return new AnonymousFuncHandler<TRequest, TResponse>(func);
        }

        public static IRequestHandler<TRequest, bool> ToRequestHandler<TRequest>(this Predicate<TRequest> predicate)
        {
            Requires.IsNotNull(predicate, nameof(predicate));

            return new AnonymousPredicateHandler<TRequest>(predicate);
        }

        public static IRequestHandler<TRequest, Nothing> ToRequestHandler<TRequest>(this Action<TRequest> action)
        {
            Requires.IsNotNull(action, nameof(action));

            return new AnonymousActionHandler<TRequest>(action);
        }

        public static Func<T, TResult> FollowedBy<T, TTemp, TResult>(this Func<T, TTemp> func, IRequestHandler<TTemp, TResult> requestHandler)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(requestHandler, nameof(requestHandler));

            return func.FollowedBy(requestHandler.Handle);
        }

        public static Func<T1, T2, TResult> FollowedBy<T1, T2, TTemp, TResult>(this Func<T1, T2, TTemp> func, IRequestHandler<TTemp, TResult> requestHandler)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(requestHandler, nameof(requestHandler));

            return func.FollowedBy(requestHandler.Handle);
        }

        public static Func<T1, T2, T3, TResult> FollowedBy<T1, T2, T3, TTemp, TResult>(this Func<T1, T2, T3, TTemp> func, IRequestHandler<TTemp, TResult> requestHandler)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(requestHandler, nameof(requestHandler));

            return func.FollowedBy(requestHandler.Handle);
        }
    }
}
