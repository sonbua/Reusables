using System.Web.Mvc;
using CqrsEventSourcingDemo.ReadModel.Tab;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.Cafe
{
    [IncludeLayoutData]
    public class WaitStaffController : BaseController
    {
        private readonly IRequestDispatcher _dispatcher;

        public WaitStaffController(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public ActionResult Todo(string id)
        {
            var tabItems = _dispatcher.DispatchQuery(new TodoListForWaiter {StaffId = id});

            ViewBag.Waiter = id;

            return View(tabItems);
        }
    }
}
