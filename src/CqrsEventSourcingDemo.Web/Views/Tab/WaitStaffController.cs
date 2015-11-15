using System;
using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Controllers.Cafe;

namespace CqrsEventSourcingDemo.Web.Controllers
{
    public class WaitStaffController : BaseController
    {
        public ActionResult Todo(Guid id)
        {
            return Content("TODO");
        }
    }
}
