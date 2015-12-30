using System;

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
    }
}
