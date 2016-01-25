using System;

namespace Reusables.Util.Extensions
{
    public static class FuncExtensions
    {
        public static Func<TIn, TResult> FollowedBy<TIn, TTemp, TResult>(this Func<TIn, TTemp> firstFunc, Func<TTemp, TResult> nextFunc)
        {
            return input => nextFunc(firstFunc(input));
        }
    }
}
