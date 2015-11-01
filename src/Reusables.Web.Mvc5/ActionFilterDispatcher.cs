using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Reusables.Web.Mvc5
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

            foreach (var attribute in ExecutingAttributesOf(filterContext.ActionDescriptor))
            {
                foreach (var actionFilter in ExecutingActionFiltersOf(attribute))
                {
                    actionFilter.OnActionExecuting(attribute, filterContext);

                    skipNextFilters = actionFilter.SkipNextFilters;

                    if (skipNextFilters)
                    {
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

            foreach (var attribute in ExecutedAttributesOf(filterContext.ActionDescriptor))
            {
                foreach (var actionFilter in ExecutedActionFiltersOf(attribute))
                {
                    actionFilter.OnActionExecuted(attribute, filterContext);

                    skipNextFilters = actionFilter.SkipNextFilters;

                    if (skipNextFilters)
                    {
                        break;
                    }
                }

                if (skipNextFilters)
                {
                    break;
                }
            }
        }

        private static IEnumerable<FilterAttribute> ExecutingAttributesOf(ActionDescriptor actionDescriptor)
        {
            var controllerCustomAttributes = actionDescriptor.ControllerDescriptor.GetCustomAttributes(inherit: true).OfType<FilterAttribute>().OrderBy(attribute => attribute.Order);
            var actionCustomAttributes = actionDescriptor.GetCustomAttributes(inherit: true).OfType<FilterAttribute>().OrderBy(attribute => attribute.Order);

            return controllerCustomAttributes.Concat(actionCustomAttributes);
        }

        private static IEnumerable<FilterAttribute> ExecutedAttributesOf(ActionDescriptor actionDescriptor)
        {
            var actionCustomAttributes = actionDescriptor.GetCustomAttributes(inherit: true).OfType<FilterAttribute>().OrderByDescending(attribute => attribute.Order);
            var controllerCustomAttributes = actionDescriptor.ControllerDescriptor.GetCustomAttributes(inherit: true).OfType<FilterAttribute>().OrderByDescending(attribute => attribute.Order);

            return actionCustomAttributes.Concat(controllerCustomAttributes);
        }

        private IEnumerable<IActionFilter> ExecutingActionFiltersOf(FilterAttribute attribute)
        {
            return ActionFiltersOf(attribute).OrderBy(filter => filter.Order);
        }

        private IEnumerable<IActionFilter> ExecutedActionFiltersOf(FilterAttribute attribute)
        {
            return ActionFiltersOf(attribute).OrderByDescending(filter => filter.Order);
        }

        private IEnumerable<IActionFilter> ActionFiltersOf(FilterAttribute attribute)
        {
            var filterType = typeof (IActionFilter<>).MakeGenericType(attribute.GetType());

            return _serviceProvider.Invoke(filterType);
        }
    }
}
