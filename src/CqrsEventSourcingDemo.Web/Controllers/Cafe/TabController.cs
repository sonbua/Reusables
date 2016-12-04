using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            return View(new OrderModel {Items = StaticData.Menu.Select(CreateOrderLineItem).ToList()});
        }

        [HttpPost]
        public ActionResult Order(int id, OrderModel orderModel)
        {
            _dispatcher.DispatchCommand(new PlaceOrder
                                        {
                                            TabId = _dispatcher.DispatchQuery(new TabIdForTable {TableNumber = id}),
                                            Items = OrderedItems(orderModel).ToList()
                                        });

            return RedirectToAction("Status", new {id = id});
        }

        public ActionResult Status(int id)
        {
            var tabStatus = _dispatcher.DispatchQuery(new TabForTable {TableNumber = id});

            return View(tabStatus);
        }

        public ActionResult MarkServed(int id, FormCollection form)
        {
            var tabId = _dispatcher.DispatchQuery(new TabIdForTable {TableNumber = id});

            var servedItemsPattern = new Regex("^served_\\d+_(\\d+)$", RegexOptions.Compiled);
            var servedMenuNumbers = form.AllKeys
                                        .Where(key => servedItemsPattern.IsMatch(key))
                                        .Where(key => form[key] != "false")
                                        .Select(key => servedItemsPattern.Match(key))
                                        .Select(match => match.Groups[1].Value)
                                        .Select(int.Parse)
                                        .ToList();

            DispatchCommands(tabId, servedMenuNumbers);

            return RedirectToAction("Status", new {id = id});
        }

        private static IEnumerable<OrderedItem> OrderedItems(OrderModel orderModel)
        {
            return orderModel.Items
                             .Where(x => x.NumberToOrder > 0)
                             .SelectMany(CreateOrderedItemsFromLineItem);
        }

        private static IEnumerable<OrderedItem> CreateOrderedItemsFromLineItem(OrderLineItem lineItem)
        {
            var menuItem = StaticData.Menu.Single(item => item.MenuNumber == lineItem.MenuNumber);

            return Enumerable.Range(0, lineItem.NumberToOrder)
                             .Select(i => CreateOrderedItem(lineItem.MenuNumber, menuItem));
        }

        private static OrderedItem CreateOrderedItem(int menuNumber, MenuItem menuItem)
        {
            return new OrderedItem
                   {
                       MenuNumber = menuNumber,
                       Description = menuItem.Description,
                       IsDrink = menuItem.IsDrink,
                       Price = menuItem.Price
                   };
        }

        private static OrderLineItem CreateOrderLineItem(MenuItem menuItem)
        {
            return new OrderLineItem
                   {
                       MenuNumber = menuItem.MenuNumber,
                       Description = menuItem.Description,
                       NumberToOrder = 0
                   };
        }

        private void DispatchCommands(Guid tabId, List<int> servedMenuNumbers)
        {
            var isDrinkLookup = StaticData.Menu.ToDictionary(menuItem => menuItem.MenuNumber, menuItem => menuItem.IsDrink);

            DispatchMarkDrinksServedCommand(tabId, servedMenuNumbers, isDrinkLookup);

            DispatchMarkFoodServedCommand(tabId, servedMenuNumbers, isDrinkLookup);
        }

        private void DispatchMarkDrinksServedCommand(Guid tabId, List<int> servedMenuNumbers, Dictionary<int, bool> isDrinkLookup)
        {
            var drinks = servedMenuNumbers.Where(item => isDrinkLookup[item]).ToList();

            if (drinks.Any())
                _dispatcher.DispatchCommand(
                    new MarkDrinksServed
                    {
                        TabId = tabId,
                        MenuNumbers = servedMenuNumbers
                    });
        }

        private void DispatchMarkFoodServedCommand(Guid tabId, List<int> servedMenuNumbers, Dictionary<int, bool> isDrinkLookup)
        {
            var food = servedMenuNumbers.Where(item => !isDrinkLookup[item]).ToList();

            if (food.Any())
                _dispatcher.DispatchCommand(
                    new MarkFoodServed
                    {
                        TabId = tabId,
                        MenuNumbers = food
                    });
        }
    }
}
