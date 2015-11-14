using System.Linq;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Pupil.ListAllPupils
{
    public class ListAllPupilsQueryHandler : IQueryHandler<ListAllPupilsQuery, PupilViewModel[]>
    {
        private readonly IViewModelDatabase _database;

        public ListAllPupilsQueryHandler(IViewModelDatabase database)
        {
            _database = database;
        }

        public PupilViewModel[] Handle(ListAllPupilsQuery query)
        {
            return _database.Set<PupilViewModel>().ToArray();
        }
    }
}
