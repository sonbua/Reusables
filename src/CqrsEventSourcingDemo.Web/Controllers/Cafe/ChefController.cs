using System.Web.Mvc;
using CqrsEventSourcingDemo.ReadModel.Tab;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.Cafe
{
    [IncludeLayoutData]
    public class ChefController : BaseController
    {
        private readonly IRequestDispatcher _dispatcher;

        public ChefController(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public ActionResult Index()
        {
            var todoLists = _dispatcher.DispatchQuery(new GetTodoLists());

            return View(todoLists);
        }

        public ActionResult MarkPrepared()
        {
            return Content("TODO");
        }
    }
}
