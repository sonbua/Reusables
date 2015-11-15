using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;

namespace CqrsEventSourcingDemo.Web.Controllers
{
    [ExceptionLogging]
    public class BaseController : Controller
    {
    }
}
