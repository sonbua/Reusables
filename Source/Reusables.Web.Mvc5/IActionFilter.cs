using System.Web.Mvc;

namespace Reusables.Web.Mvc5
{
    public interface IActionFilter
    {
        int Order { get; }

        /// <summary>
        /// Once this flag is set to <c>true</c>, all following executions of <see cref="IActionFilter"/> instances in the queue will be skipped.
        /// </summary>
        bool SkipNextFilters { get; }

        void OnActionExecuting(object attribute, ActionExecutingContext filterContext);

        void OnActionExecuted(object attribute, ActionExecutedContext filterContext);
    }

    public interface IActionFilter<TFilterAttribute> : IActionFilter where TFilterAttribute : FilterAttribute
    {
        void OnActionExecuting(TFilterAttribute attribute, ActionExecutingContext filterContext);

        void OnActionExecuted(TFilterAttribute attribute, ActionExecutedContext filterContext);
    }
}
