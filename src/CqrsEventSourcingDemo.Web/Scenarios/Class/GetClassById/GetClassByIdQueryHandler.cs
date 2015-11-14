using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.GetClassById
{
    public class GetClassByIdQueryHandler : IQueryHandler<GetClassByIdQuery, ClassViewModel>
    {
        private readonly IViewModelDatabase _database;

        public GetClassByIdQueryHandler(IViewModelDatabase database)
        {
            _database = database;
        }

        public ClassViewModel Handle(GetClassByIdQuery query)
        {
            return _database.Set<ClassViewModel>().GetById(query.Id);
        }
    }
}
