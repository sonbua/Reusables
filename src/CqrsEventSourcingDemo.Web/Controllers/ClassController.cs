using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CqrsEventSourcingDemo.Web.Abstractions.Handlers;
using CqrsEventSourcingDemo.Web.Scenarios.Class.ListAllClasses;
using Reusables.Cqrs;
using Reusables.DataAnnotations;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

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

    public class AddNewClassCommand : Command
    {
        public Guid ClassId { get; set; }

        [Required]
        public string ClassName { get; set; }
    }

    public class ClassService : CommandHandler<ClassAggregate, AddNewClassCommand>
    {
        public ClassService(IRepository repository) : base(repository)
        {
        }

        public override void Handle(AddNewClassCommand command)
        {
            var id = Guid.NewGuid();

            Act(id, aggregate => aggregate.AddNew(id, command.ClassName));

            command.ClassId = id;
        }
    }

    public class ClassAggregate : Aggregate
    {
        public ClassAggregate(IEnumerable<Event> history)
        {
            foreach (var @event in history)
            {
                Apply(@event);
            }
        }

        public string Name { get; set; }

        public void AddNew(Guid id, string className)
        {
            Publish(new NewClassAdded {Id = id, ClassName = className});
        }

        private void Publish(Event @event)
        {
            UncommittedEvents.Add(@event);

            Apply(@event);
        }

        private void Apply(Event @event)
        {
            Version++;

            this.InvokeEventOptional(@event);
        }

        private void When(NewClassAdded @event)
        {
            Id = @event.Id;
            Name = @event.ClassName;
        }
    }

    public class NewClassAdded : Event
    {
        public Guid Id { get; set; }

        public string ClassName { get; set; }
    }
}
