using System.Linq;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.Queries
{
    public class ClassQueryHandlers : IQueryHandler<GetClassByIdQuery, ClassViewModel>,
                                      IQueryHandler<ListAllClassesQuery, ClassViewModel[]>
    {
        private readonly IViewModelDatabase _database;

        public ClassQueryHandlers(IViewModelDatabase database)
        {
            _database = database;
        }

        public ClassViewModel Handle(GetClassByIdQuery query)
        {
            return _database.Set<ClassViewModel>().GetById(query.Id);
        }

        public ClassViewModel[] Handle(ListAllClassesQuery query)
        {
            return _database.Set<ClassViewModel>().ToArray();
        }
    }
}
