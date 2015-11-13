using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions.Views
{
    public class InMemoryViewDatabase : IViewDatabase
    {
        public IViewSet<TView> Set<TView>() where TView : View
        {
            return ViewCache<TView>.Instance;
        }

        private static class ViewCache<TView> where TView : View
        {
            private static IViewSet<TView> _instance;

            public static IViewSet<TView> Instance => _instance ?? (_instance = new InMemoryViewSet<TView>());
        }
    }
}
