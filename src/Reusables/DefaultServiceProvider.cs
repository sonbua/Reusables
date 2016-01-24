using System;

namespace Reusables
{
    public static class DefaultServiceProvider
    {
        private static IServiceProvider _resolver;

        /// <summary>
        /// This plays as a dependency resolver. It should be set while the application is bootstrapping.
        /// </summary>
        public static IServiceProvider Current
        {
            get
            {
                if (_resolver == null)
                {
                    throw new MemberAccessException($"'{nameof(_resolver)}' has not been set.");
                }

                return _resolver;
            }
            set { _resolver = value; }
        }
    }
}
