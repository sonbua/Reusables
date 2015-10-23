using System.Web.Mvc;

namespace Reusables.Web.Mvc5
{
    public abstract class ActionFilterBase<TAttribute> : IActionFilter<TAttribute> where TAttribute : BaseAttribute
    {
        public int Order { get; set; }

        public bool SkipNextFilters { get; set; }

        public void OnActionExecuting(object attribute, ActionExecutingContext filterContext)
        {
            OnActionExecuting((TAttribute) attribute, filterContext);
        }

        public void OnActionExecuted(object attribute, ActionExecutedContext filterContext)
        {
            OnActionExecuted((TAttribute) attribute, filterContext);
        }

        public abstract void OnActionExecuting(TAttribute attribute, ActionExecutingContext filterContext);

        public abstract void OnActionExecuted(TAttribute attribute, ActionExecutedContext filterContext);
    }
}
