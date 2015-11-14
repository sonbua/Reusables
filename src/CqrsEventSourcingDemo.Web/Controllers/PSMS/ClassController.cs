using System;
using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass;
using CqrsEventSourcingDemo.Web.Scenarios.Class.GetClassById;
using CqrsEventSourcingDemo.Web.Scenarios.Class.ListAllClasses;
using CqrsEventSourcingDemo.Web.Scenarios.Class.RemoveClass;
using CqrsEventSourcingDemo.Web.Scenarios.Class.RenameClass;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Controllers.PSMS
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

        public ActionResult Rename(Guid id)
        {
            var query = new GetClassByIdQuery {Id = id};

            var classView = _dispatcher.DispatchQuery(query);

            return View(classView);
        }

        [HttpPost]
        public ActionResult Rename(Guid id, string newName)
        {
            var command = new RenameClassCommand
                          {
                              Id = id,
                              NewName = newName
                          };

            _dispatcher.DispatchCommand(command);

            return RedirectToAction("Index");
        }

        public ActionResult Remove(Guid id)
        {
            var query = new GetClassByIdQuery {Id = id};

            var classView = _dispatcher.DispatchQuery(query);

            return View(classView);
        }

        [HttpPost]
        public ActionResult RemoveImpl(Guid id)
        {
            var command = new RemoveClassCommand {Id = id};

            _dispatcher.DispatchCommand(command);

            return RedirectToAction("Index");
        }
    }
}
