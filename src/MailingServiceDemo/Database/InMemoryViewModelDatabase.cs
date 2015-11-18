using System;
using System.Collections.Generic;

namespace MailingServiceDemo.Database
{
    public class InMemoryViewModelDatabase : IViewModelDatabase
    {
        public ISet<Type> CachedSetTypes { get; }

        public InMemoryViewModelDatabase()
        {
            CachedSetTypes = new HashSet<Type>();
        }

        public IViewModelSet<TViewModel> Set<TViewModel>() where TViewModel : ViewModel
        {
            if (CachedSetTypes.Contains(typeof (TViewModel)))
            {
                return Cache<TViewModel>.Instance;
            }

            CachedSetTypes.Add(typeof (TViewModel));
            Cache<TViewModel>.Instance = null;

            return Cache<TViewModel>.Instance;
        }

        public void Clean()
        {
            CachedSetTypes.Clear();
        }

        private class Cache<TViewModel> where TViewModel : ViewModel
        {
            private static IViewModelSet<TViewModel> _instance;

            public static IViewModelSet<TViewModel> Instance
            {
                get { return _instance ?? (_instance = new InMemoryViewModelSet<TViewModel>()); }
                set { _instance = value; }
            }
        }
    }
}
