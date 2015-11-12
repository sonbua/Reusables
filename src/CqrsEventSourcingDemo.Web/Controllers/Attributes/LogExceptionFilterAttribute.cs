using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Reusables.Diagnostics.Logging;
using Reusables.Web.Mvc5;

namespace CqrsEventSourcingDemo.Web.Controllers.Attributes
{
    public class LogExceptionFilterAttribute : ActionFilterBase<ExceptionLoggingAttribute>
    {
        private readonly ILogger _logger;

        public LogExceptionFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ExceptionLoggingAttribute attribute, ActionExecutingContext filterContext)
        {
        }

        public override void OnActionExecuted(ExceptionLoggingAttribute attribute, ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                return;
            }

            _logger.Error(filterContext.Exception, ExtractRequestInfo(filterContext));

            RedirectToErrorPage(filterContext);
        }

        private static string ExtractRequestInfo(ActionExecutedContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;

            var requestInfo = new {RequestInfo = request.Params.AllKeys.Select(key => new {Key = key, Value = request.Params[key]})};

            return JsonConvert.SerializeObject(requestInfo);
        }

        private static void RedirectToErrorPage(ActionExecutedContext filterContext)
        {
            if (!filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            filterContext.Result = new ViewResult
                                   {
                                       ViewName = "Error",
                                       MasterName = string.Empty,
                                       ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                                       TempData = filterContext.Controller.TempData
                                   };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
