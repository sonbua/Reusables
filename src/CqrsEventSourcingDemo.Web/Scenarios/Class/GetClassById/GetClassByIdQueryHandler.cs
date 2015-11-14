using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.GetClassById
{
    public class GetClassByIdQueryHandler : IQueryHandler<GetClassByIdQuery, ClassView>
    {
        private readonly IViewDatabase _viewDatabase;

        public GetClassByIdQueryHandler(IViewDatabase viewDatabase)
        {
            _viewDatabase = viewDatabase;
        }

        public ClassView Handle(GetClassByIdQuery query)
        {
            return _viewDatabase.Set<ClassView>().GetById(query.Id);
        }
    }
}
