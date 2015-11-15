using CqrsEventSourcingDemo.ReadModel;

namespace CqrsEventSourcingDemo.Infrastructure
{
    public class InMemoryViewModelDatabase : IViewModelDatabase
    {
        public IViewModelSet<TViewModel> Set<TViewModel>() where TViewModel : ViewModel
        {
            return Cache<TViewModel>.Instance;
        }

        private static class Cache<TViewModel> where TViewModel : ViewModel
        {
            private static IViewModelSet<TViewModel> _instance;

            public static IViewModelSet<TViewModel> Instance => _instance ?? (_instance = new InMemoryViewModelSet<TViewModel>());
        }
    }
}
