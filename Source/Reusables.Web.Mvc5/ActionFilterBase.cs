using System.Web.Mvc;

namespace Reusables.Web.Mvc5
{
    public abstract class ActionFilterBase<TFilterAttribute> : IActionFilter<TFilterAttribute> where TFilterAttribute : FilterAttribute
    {
        public int Order { get; set; }

        public bool SkipNextFilters { get; set; }

        public void OnActionExecuting(object attribute, ActionExecutingContext filterContext)
        {
            OnActionExecuting((TFilterAttribute) attribute, filterContext);
        }

        public void OnActionExecuted(object attribute, ActionExecutedContext filterContext)
        {
            OnActionExecuted((TFilterAttribute) attribute, filterContext);
        }

        public abstract void OnActionExecuting(TFilterAttribute attribute, ActionExecutingContext filterContext);

        public abstract void OnActionExecuted(TFilterAttribute attribute, ActionExecutedContext filterContext);
    }
}
