using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reusables.Util.Extensions
{
    /// <summary>
    /// All extension methods related to IEnumarable&lt;T&gt;.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable&lt;T&gt;"/> to check for nullness or emptiness.</param>
        /// <returns><c>true</c> if the source sequence contains any elements; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return true;
            }

            using (var enumerator = source.GetEnumerator())
            {
                return !enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Repeats an action for each element in an array or an object collection that implements the System.Collections.IEnumerable or System.Collections.Generic.IEnumerable&lt;T&gt; interface.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to invoke a function on.</param>
        /// <param name="action">A function to apply to each object of the sequence.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="action"/> is null.</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Build a string from a sequence of objects.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each object of the sequence.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static string BuildString<T>(this IEnumerable<T> source, Func<T, string> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            return BuildStringImpl(source, selector);
        }

        private static string BuildStringImpl<T>(IEnumerable<T> source, Func<T, string> func)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in source)
            {
                stringBuilder.Append(func(item));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Concatenates the members of a constructed collection of arbitrary objects, using the specified separator between each member.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A collection that contains the objects to concatenate.</param>
        /// <param name="selector">A transform function to apply to each object of the sequence.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <returns>A string that consists of the members of values delimited by the separator string. If values has no members, the method returns <see cref="string.Empty"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static string Join<T>(this IEnumerable<T> source, Func<T, string> selector, string separator)
        {
            return string.Join(separator, source.Select(selector));
        }

        /// <summary>
        /// Check whether a collection contains an specified item.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="collection"/>.</typeparam>
        /// <param name="item">The item to check.</param>
        /// <param name="collection">A sequence in which to locate a value.</param>
        /// <returns><c>true</c> if item is in the collection, otherwise <c>false</c>. Always returns <c>false</c> if item is null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is null.</exception>
        public static bool IsIn<T>(this T item, IEnumerable<T> collection)
        {
            return collection.Contains(item);
        }
    }
}