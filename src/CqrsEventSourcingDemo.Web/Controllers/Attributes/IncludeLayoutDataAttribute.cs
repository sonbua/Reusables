using System.Collections.Generic;
using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Domain.ReadModels.Tab;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.Attributes
{
    public class IncludeLayoutDataAttribute : ActionFilterAttribute
    {
        private readonly IRequestDispatcher _dispatcher = DependencyResolver.Current.GetService<IRequestDispatcher>();

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult != null)
            {
                var bag = viewResult.ViewBag;

                bag.WaitStaff = StaticData.WaitStaff;
                bag.ActiveTables = _dispatcher.DispatchQuery(new ActiveTableNumbersQuery());
            }
        }
    }

    public class StaticData
    {
        public static List<MenuItem> Menu =
            new List<MenuItem>
            {
                new MenuItem {MenuNumber = 1, Description = "Coke", Price = 1.50M, IsDrink = true},
                new MenuItem {MenuNumber = 2, Description = "Green Tea", Price = 1.90M, IsDrink = true},
                new MenuItem {MenuNumber = 3, Description = "Freshly Ground Coffee", Price = 2.00M, IsDrink = true},
                new MenuItem {MenuNumber = 4, Description = "Czech Pilsner", Price = 3.50M, IsDrink = true},
                new MenuItem {MenuNumber = 5, Description = "Yeti Stout", Price = 4.50M, IsDrink = true},
                new MenuItem {MenuNumber = 10, Description = "Mushroom & Bacon Pasta", Price = 6.00M},
                new MenuItem {MenuNumber = 11, Description = "Chili Con Carne", Price = 7.50M},
                new MenuItem {MenuNumber = 12, Description = "Borsch With Smetana", Price = 4.50M},
                new MenuItem {MenuNumber = 13, Description = "Lamb Skewers with Tatziki", Price = 8.00M},
                new MenuItem {MenuNumber = 14, Description = "Beef Stroganoff", Price = 8.50M},
            };

        public static List<string> WaitStaff = new List<string> {"Jack", "Lena", "Pedro", "Anastasia"};
    }

    public class MenuItem
    {
        public int MenuNumber { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsDrink { get; set; }
    }
}
