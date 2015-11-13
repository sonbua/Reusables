using System;
using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Abstractions.Views;
using CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass;
using CqrsEventSourcingDemo.Web.Scenarios.Class.ListAllClasses;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Controllers
{
    public class ClassController : BaseController
    {
        private readonly IRequestDispatcher _dispatcher;

        public ClassController(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public ActionResult Index()
        {
            var query = new ListAllClassesQuery();

            var classes = _dispatcher.DispatchQuery(query);

            return View(classes);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string className)
        {
            var command = new AddNewClassCommand
                          {
                              ClassName = className
                          };

            _dispatcher.DispatchCommand(command);

            return RedirectToAction("Index");
        }
    }

}
