using System;
using System.Collections.Generic;

namespace Reusables.Diagnostics.Contracts
{
    public static class Requires
    {
        public static void IsNotNull(object instance, string paramName)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void IsNotNullOrEmpty<T>(IEnumerable<T> source, string paramName)
        {
            IsNotNull(source, paramName);

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentException($"{paramName} should not be empty.");
                }
            }
        }
    }
}
