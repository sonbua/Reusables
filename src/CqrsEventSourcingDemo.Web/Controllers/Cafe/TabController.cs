using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;
using CqrsEventSourcingDemo.Web.Domain.Commands.Tab;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.Cafe
{
    [IncludeLayoutData]
    public class TabController : BaseController
    {
        private readonly IRequestDispatcher _dispatcher;

        public TabController(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public ActionResult Open()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Open(OpenTab command)
        {
            _dispatcher.DispatchCommand(command);

            return RedirectToAction("Order", new {id = command.TableNumber});
        }

        public ActionResult Order(int id)
        {
            return Content("TODO");
        }

        public ActionResult Status(int id)
        {
            return Content("TODO");
        }
    }
}
