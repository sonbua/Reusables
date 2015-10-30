using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reusables.Util.Extensions
{
    /// <summary>
    /// All extension methods related to Assembly.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Get all loadable types from a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly to load types from.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}