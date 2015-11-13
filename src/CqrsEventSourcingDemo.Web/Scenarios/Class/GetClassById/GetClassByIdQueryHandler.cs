using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.GetClassById
{
    public class GetClassByIdQueryHandler : QueryHandler<GetClassByIdQuery, ClassView>
    {
        private readonly IViewDatabase _viewDatabase;

        public GetClassByIdQueryHandler(IViewDatabase viewDatabase)
        {
            _viewDatabase = viewDatabase;
        }

        public override ClassView Handle(GetClassByIdQuery query)
        {
            return _viewDatabase.Set<ClassView>().GetById(query.Id);
        }
    }
}
