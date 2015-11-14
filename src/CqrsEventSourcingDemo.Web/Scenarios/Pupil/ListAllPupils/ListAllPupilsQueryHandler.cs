using System.Linq;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Pupil.ListAllPupils
{
    public class ListAllPupilsQueryHandler : IQueryHandler<ListAllPupilsQuery, PupilViewModel[]>
    {
        private readonly IViewDatabase _viewDatabase;

        public ListAllPupilsQueryHandler(IViewDatabase viewDatabase)
        {
            _viewDatabase = viewDatabase;
        }

        public PupilViewModel[] Handle(ListAllPupilsQuery query)
        {
            return _viewDatabase.Set<PupilViewModel>().ToArray();
        }
    }
}
