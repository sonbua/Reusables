using System;
using System.Collections.Generic;
using System.Linq;
using Reusables.Diagnostics.Contracts;

namespace Reusables.Util.Extensions
{
    public static class FuncExtensions
    {
        public static Func<T, TResult> FollowedBy<T, TTemp, TResult>(this Func<T, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return arg => nextFunc(firstFunc(arg));
        }

        public static Func<T1, T2, TResult> FollowedBy<T1, T2, TTemp, TResult>(this Func<T1, T2, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2) => nextFunc(firstFunc(arg1, arg2));
        }

        public static Func<T1, T2, T3, TResult> FollowedBy<T1, T2, T3, TTemp, TResult>(this Func<T1, T2, T3, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3) => nextFunc(firstFunc(arg1, arg2, arg3));
        }

        public static Func<T1, T2, T3, T4, TResult> FollowedBy<T1, T2, T3, T4, TTemp, TResult>(this Func<T1, T2, T3, T4, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4) => nextFunc(firstFunc(arg1, arg2, arg3, arg4));
        }

        public static Func<T1, T2, T3, T4, T5, TResult> FollowedBy<T1, T2, T3, T4, T5, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5));
        }

        public static Func<T1, T2, T3, T4, T5, T6, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> FollowedBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TTemp, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            Requires.IsNotNull(firstFunc, nameof(firstFunc));
            Requires.IsNotNull(nextFunc, nameof(nextFunc));

            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => nextFunc(firstFunc(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        public static Func<T, IEnumerable<TResult>> Select<T, TSource, TResult>(this Func<T, IEnumerable<TSource>> func, Func<TSource, TResult> selector)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(selector, nameof(selector));

            return arg => func(arg).Select(selector);
        }

        public static Func<T1, T2, IEnumerable<TResult>> Select<T1, T2, TSource, TResult>(this Func<T1, T2, IEnumerable<TSource>> func, Func<TSource, TResult> selector)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(selector, nameof(selector));

            return (arg1, arg2) => func(arg1, arg2).Select(selector);
        }

        public static Func<T, IEnumerable<TResult>> Where<T, TResult>(this Func<T, IEnumerable<TResult>> func, Func<TResult, bool> predicate)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(predicate, nameof(predicate));

            return arg => func(arg).Where(predicate);
        }

        public static Func<T1, T2, IEnumerable<TResult>> Where<T1, T2, TResult>(this Func<T1, T2, IEnumerable<TResult>> func, Func<TResult, bool> predicate)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(predicate, nameof(predicate));

            return (arg1, arg2) => func(arg1, arg2).Where(predicate);
        }

        public static Func<T, Nothing> ForEach<T, TSource>(this Func<T, IEnumerable<TSource>> func, Action<TSource> action)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(action, nameof(action));

            return arg =>
                   {
                       func(arg).ForEach(action);

                       return new Nothing();
                   };
        }

        public static Func<T1, T2, Nothing> ForEach<T1, T2, TSource>(this Func<T1, T2, IEnumerable<TSource>> func, Action<TSource> action)
        {
            Requires.IsNotNull(func, nameof(func));
            Requires.IsNotNull(action, nameof(action));

            return (arg1, arg2) =>
                   {
                       func(arg1, arg2).ForEach(action);

                       return new Nothing();
                   };
        }
    }
}
