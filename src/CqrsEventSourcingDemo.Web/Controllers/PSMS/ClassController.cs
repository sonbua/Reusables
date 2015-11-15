using System;
using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Scenarios.Class.Commands;
using CqrsEventSourcingDemo.Web.Scenarios.Class.Queries;
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

            var viewModels = _dispatcher.DispatchQuery(query);

            return View(viewModels);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string className)
        {
            var command = new AddNewClass
                          {
                              ClassName = className
                          };

            _dispatcher.DispatchCommand(command);

            return RedirectToAction("Index");
        }

        public ActionResult Rename(Guid id)
        {
            var query = new GetClassByIdQuery {Id = id};

            var viewModel = _dispatcher.DispatchQuery(query);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Rename(Guid id, string newName)
        {
            var command = new RenameClass
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

            var viewModel = _dispatcher.DispatchQuery(query);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RemoveImpl(Guid id)
        {
            var command = new RemoveClass {Id = id};

            _dispatcher.DispatchCommand(command);

            return RedirectToAction("Index");
        }
    }
}
