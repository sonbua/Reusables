using System;
using System.Collections.Generic;
using System.Linq;
using FastMember;
using Reusables.Diagnostics.Contracts;

namespace Reusables.Util.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Extracts all run-time type's public member names and associated values from an object instance to a dictionary. Does not support types that contain public static member(s), and will throw <see cref="ArgumentOutOfRangeException"/> in this case.
        /// </summary>
        /// <param name="instance">The object to retrieve property values.</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary<T>(this T instance)
        {
            Requires.IsNotNull(instance, nameof(instance));

            var typeAccessor = TypeAccessor.Create(instance.GetType());

            return typeAccessor.GetMembers()
                               .ToDictionary(member => member.Name,
                                             member => typeAccessor[instance, member.Name]);
        }

        /// <summary>
        /// Gets order of the given instance by its compile-time type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static int GetOrder<T>(this T instance)
        {
            Requires.IsNotNull(instance, nameof(instance));

            return typeof(T).GetOrderImpl();
        }

        /// <summary>
        /// Gets order of the given instance by its run-time type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static int GetRuntimeOrder<T>(this T instance)
        {
            Requires.IsNotNull(instance, nameof(instance));

            return instance.GetType().GetOrderImpl();
        }

        private static int GetOrderImpl(this Type type)
        {
            var orderAttributes = type.GetCustomAttributes(typeof(OrderAttribute), true);

            if (orderAttributes.Length == 0)
            {
                return 0;
            }

            return ((OrderAttribute) orderAttributes[0]).Value;
        }
    }
}
