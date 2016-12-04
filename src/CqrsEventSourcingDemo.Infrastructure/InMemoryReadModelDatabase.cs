using CqrsEventSourcingDemo.ReadModel;

namespace CqrsEventSourcingDemo.Infrastructure
{
    public class InMemoryReadModelDatabase : IReadModelDatabase
    {
        public IReadModelSet<TReadModel> Set<TReadModel>() where TReadModel : ReadModel.ReadModel
        {
            return Cache<TReadModel>.Instance;
        }

        private static class Cache<TReadModel> where TReadModel : ReadModel.ReadModel
        {
            private static IReadModelSet<TReadModel> _instance;

            public static IReadModelSet<TReadModel> Instance => _instance ?? (_instance = new InMemoryReadModelSet<TReadModel>());
        }
    }
}