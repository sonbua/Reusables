using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;

namespace CqrsEventSourcingDemo.Web.Controllers.Cafe
{
    [ExceptionLogging]
    public class BaseController : Controller
    {
    }
}
