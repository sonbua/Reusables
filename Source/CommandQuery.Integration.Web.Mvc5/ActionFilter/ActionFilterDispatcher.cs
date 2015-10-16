using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CommandQuery.Integration.Web.Mvc5.ActionFilter
{
    public class ActionFilterDispatcher : System.Web.Mvc.IActionFilter
    {
        private readonly Func<Type, IEnumerable<IActionFilter>> _serviceProvider;

        public ActionFilterDispatcher(Func<Type, IEnumerable<IActionFilter>> serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var skipNextFilters = false;

            foreach (var attribute in AttributesOf(filterContext.ActionDescriptor))
            {
                foreach (var actionFilter in ActionFiltersOf(attribute))
                {
                    actionFilter.OnActionExecuting(attribute, filterContext);

                    if (actionFilter.SkipNextFilters)
                    {
                        skipNextFilters = true;
                        break;
                    }
                }

                if (skipNextFilters)
                {
                    break;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var skipNextFilters = false;

            foreach (var attribute in AttributesOf(filterContext.ActionDescriptor))
            {
                foreach (var actionFilter in ActionFiltersOf(attribute))
                {
                    actionFilter.OnActionExecuted(attribute, filterContext);

                    if (actionFilter.SkipNextFilters)
                    {
                        skipNextFilters = true;
                        break;
                    }
                }

                if (skipNextFilters)
                {
                    break;
                }
            }
        }

        private static IEnumerable<BaseAttribute> AttributesOf(ActionDescriptor actionDescriptor)
        {
            var controllerCustomAttributes = actionDescriptor.ControllerDescriptor.GetCustomAttributes(inherit: true).OfType<BaseAttribute>().OrderBy(attribute => attribute.Order);
            var actionCustomAttributes = actionDescriptor.GetCustomAttributes(inherit: true).OfType<BaseAttribute>().OrderBy(attribute => attribute.Order);

            return controllerCustomAttributes.Concat(actionCustomAttributes);
        }

        private IOrderedEnumerable<IActionFilter> ActionFiltersOf(BaseAttribute attribute)
        {
            var filterType = typeof (IActionFilter<>).MakeGenericType(attribute.GetType());

            return _serviceProvider.Invoke(filterType).OrderBy(filter => filter.Order);
        }
    }
}