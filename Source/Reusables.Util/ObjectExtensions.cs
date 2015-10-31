using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reusables.Util.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Extract all public properties' names and associated values from an object instance to a dictionary. The instance type must have a public parameterless constructor.
        /// </summary>
        /// <param name="instance">The object to retrieve property values.</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary<T>(this T instance) where T : new()
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            return instance.GetType()
                           .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                           .ToDictionary(x => x.Name, x => x.GetValue(instance));
        }
    }
}
