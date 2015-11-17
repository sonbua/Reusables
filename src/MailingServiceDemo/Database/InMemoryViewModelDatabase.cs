using System;
using System.Collections.Generic;

namespace MailingServiceDemo.Database
{
    public class InMemoryViewModelDatabase : IViewModelDatabase
    {
        private readonly ISet<Type> _cachedTypes;

        public InMemoryViewModelDatabase()
        {
            _cachedTypes = new HashSet<Type>();
        }

        public IViewModelSet<TViewModel> Set<TViewModel>() where TViewModel : ViewModel
        {
            if (_cachedTypes.Contains(typeof (TViewModel)))
            {
                return Cache<TViewModel>.Instance;
            }

            _cachedTypes.Add(typeof (TViewModel));
            Cache<TViewModel>.Instance = null;

            return Cache<TViewModel>.Instance;
        }

        public void Clean()
        {
            _cachedTypes.Clear();
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
