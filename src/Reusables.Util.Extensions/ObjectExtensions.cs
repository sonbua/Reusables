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
    }
}
