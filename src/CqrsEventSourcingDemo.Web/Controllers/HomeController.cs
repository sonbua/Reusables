using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;

namespace CqrsEventSourcingDemo.Web.Controllers
{
    [IncludeLayoutData]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
