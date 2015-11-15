using System.Linq;
using System.Web.Mvc;
using CqrsEventSourcingDemo.Command.Tab;
using CqrsEventSourcingDemo.Event.Tab;
using CqrsEventSourcingDemo.ReadModel.Tab;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;
using CqrsEventSourcingDemo.Web.Models;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.Cafe
{
    [IncludeLayoutData]
    public class TabController : BaseController
    {
        private readonly IRequestDispatcher _dispatcher;

        public TabController(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public ActionResult Open()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Open(OpenTab command)
        {
            _dispatcher.DispatchCommand(command);

            return RedirectToAction("Order", new {id = command.TableNumber});
        }

        public ActionResult Order(int id)
        {
            return View(new OrderModel
                        {
                            Items = StaticData.Menu
                                              .Select(x => new OrderLineItem
                                                           {
                                                               MenuNumber = x.MenuNumber,
                                                               Description = x.Description,
                                                               NumberToOrder = 0
                                                           })
                                              .ToList()
                        });
        }

        [HttpPost]
        public ActionResult Order(int id, OrderModel orderModel)
        {
            var tabId = _dispatcher.DispatchQuery(new TabIdForTableQuery {TableNumber = id});

            _dispatcher.DispatchCommand(new PlaceOrder
                                        {
                                            TabId = tabId,
                                            Items = orderModel.Items
                                                              .Where(lineItem => lineItem.NumberToOrder > 0)
                                                              .Select(lineItem => new
                                                                                  {
                                                                                      LineItem = lineItem,
                                                                                      MenuItem = StaticData.Menu.Single(menuItem => menuItem.MenuNumber == lineItem.MenuNumber)
                                                                                  })
                                                              .Select(x => new OrderedItem
                                                                           {
                                                                               MenuNumber = x.LineItem.MenuNumber,
                                                                               Description = x.MenuItem.Description,
                                                                               IsDrink = x.MenuItem.IsDrink,
                                                                               Price = x.MenuItem.Price
                                                                           })
                                                              .ToList()
                                        });

            return RedirectToAction("Status", new {id = id});
        }

        public ActionResult Status(int id)
        {
            return Content("TODO");
        }
    }
}
