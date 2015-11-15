using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Controllers.PSMS;

namespace CqrsEventSourcingDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AddMockData();

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

        private void AddMockData()
        {
            var classController = DependencyResolver.Current.GetService<ClassController>();

            classController.Add("Toan hoc");
            classController.Add("Vat li");
            classController.Add("Hoa hoc");
            classController.Add("Sinh hoc");
        }
    }
}
