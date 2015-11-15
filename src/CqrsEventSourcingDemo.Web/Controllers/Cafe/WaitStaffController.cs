using System;
using System.Web.Mvc;

namespace CqrsEventSourcingDemo.Web.Controllers.Cafe
{
    public class WaitStaffController : BaseController
    {
        public ActionResult Todo(Guid id)
        {
            return Content("TODO");
        }
    }
}
