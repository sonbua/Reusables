using System.Linq;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.ListAllClasses
{
    public class ListAllClassesQueryHandler : IQueryHandler<ListAllClassesQuery, ClassViewModel[]>
    {
        private readonly IViewModelDatabase _database;

        public ListAllClassesQueryHandler(IViewModelDatabase database)
        {
            _database = database;
        }

        public ClassViewModel[] Handle(ListAllClassesQuery query)
        {
            return _database.Set<ClassViewModel>().ToArray();
        }
    }
}
