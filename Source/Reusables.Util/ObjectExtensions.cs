using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reusables.Util.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Extract all public properties' names and associated values from an object instance to a dictionary. The instance must have an empty constructor.
        /// </summary>
        /// <param name="source">The object to retrieve property values.</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(this object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.GetType()
                         .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                         .ToDictionary(x => x.Name, x => x.GetValue(source));
        }
    }
}
