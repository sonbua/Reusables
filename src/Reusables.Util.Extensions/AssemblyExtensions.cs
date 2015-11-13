using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reusables.Diagnostics.Contracts;

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
            Requires.IsNotNull(assembly, nameof(assembly));

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