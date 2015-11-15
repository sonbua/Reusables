using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;

namespace CqrsEventSourcingDemo.Web.Controllers.Cafe
{
    [IncludeLayoutData]
    public class TabController : BaseController
    {
        public ActionResult Open()
        {
            return Content("TODO");
        }
    }
}
