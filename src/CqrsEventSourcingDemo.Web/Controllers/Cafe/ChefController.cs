using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using CqrsEventSourcingDemo.Command.Tab;
using CqrsEventSourcingDemo.Event.Tab;
using CqrsEventSourcingDemo.ReadModel.Tab;
using CqrsEventSourcingDemo.Web.Controllers.Attributes;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.Cafe
{
    [IncludeLayoutData]
    public class ChefController : BaseController
    {
        private readonly IRequestDispatcher _dispatcher;

        public ChefController(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public ActionResult Index()
        {
            var todoLists = _dispatcher.DispatchQuery(new GetTodoLists());

            return View(todoLists);
        }

        public ActionResult MarkPrepared(Guid tabId, FormCollection form)
        {
            var servedItemsPattern = new Regex("^prepared_\\d+_(\\d+)$", RegexOptions.Compiled);
            var servedMenuNumbers =
                form.AllKeys
                    .Where(key => servedItemsPattern.IsMatch(key))
                    .Where(key => form[key] != "false")
                    .Select(key => servedItemsPattern.Match(key))
                    .Select(match => match.Groups[1].Value)
                    .Select(int.Parse)
                    .ToList();

            _dispatcher.DispatchCommand(
                new MarkFoodPrepared
                {
                    TabId = tabId,
                    MenuNumbers = servedMenuNumbers
                });

            return RedirectToAction("Index");
        }
    }
}