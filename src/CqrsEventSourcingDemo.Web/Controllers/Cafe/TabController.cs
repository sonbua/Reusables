﻿using System;
using System.Collections.Generic;
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
                                              .Select(menuItem => new OrderLineItem
                                                                  {
                                                                      MenuNumber = menuItem.MenuNumber,
                                                                      Description = menuItem.Description,
                                                                      NumberToOrder = 0
                                                                  })
                                              .ToList()
                        });
        }

        [HttpPost]
        public ActionResult Order(int id, OrderModel orderModel)
        {
            _dispatcher.DispatchCommand(new PlaceOrder
                                        {
                                            TabId = _dispatcher.DispatchQuery(new TabIdForTableQuery {TableNumber = id}),
                                            Items = OrderedItems(orderModel).ToList()
                                        });

            return RedirectToAction("Status", new {id = id});
        }

        private static IEnumerable<OrderedItem> OrderedItems(OrderModel orderModel)
        {
            foreach (var lineItem in orderModel.Items.Where(x => x.NumberToOrder > 0))
            {
                var menuItem = StaticData.Menu.Single(item => item.MenuNumber == lineItem.MenuNumber);

                for (var i = 0; i < lineItem.NumberToOrder; i++)
                {
                    yield return new OrderedItem
                                 {
                                     MenuNumber = lineItem.MenuNumber,
                                     Description = menuItem.Description,
                                     IsDrink = menuItem.IsDrink,
                                     Price = menuItem.Price
                                 };
                }
            }
        }

        public ActionResult Status(int id)
        {
            var tabStatus = _dispatcher.DispatchQuery(new TabForTableQuery {TableNumber = id});

            return View(tabStatus);
        }

        public ActionResult MarkServed(int id)
        {
            return Content("TODO");
        }
    }
}
