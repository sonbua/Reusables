using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Scenarios.Pupil.ListAllPupils;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.PSMS
{
    public class PupilController : BaseController
    {
        private readonly IRequestDispatcher _dispatcher;

        public PupilController(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public ActionResult Index()
        {
            var query = new ListAllPupilsQuery();

            var viewModels = _dispatcher.DispatchQuery(query);

            return View(viewModels);
        }
    }
}
