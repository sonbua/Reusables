using System.Web.Mvc;
using CqrsEventSourcingDemo.ReadModel.Tab;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.Attributes
{
    internal class IncludeLayoutDataAttribute : ActionFilterAttribute
    {
        private readonly IRequestDispatcher _dispatcher;

        public IncludeLayoutDataAttribute()
        {
            _dispatcher = DependencyResolver.Current.GetService<IRequestDispatcher>();
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult != null)
            {
                var bag = viewResult.ViewBag;

                bag.WaitStaff = StaticData.WaitStaff;
                bag.ActiveTables = _dispatcher.DispatchQuery(new ActiveTableNumbers());
            }
        }
    }
}