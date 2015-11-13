using System;
using CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class
{
    public class ClassViewObserver : Subscriber<NewClassAdded>
    {
        private readonly IViewDatabase _viewDatabase;

        public ClassViewObserver(IViewDatabase viewDatabase)
        {
            _viewDatabase = viewDatabase;
        }

        public string Name { get; set; }

        public Guid Id { get; set; }

        public override void Handle(NewClassAdded @event)
        {
            _viewDatabase.Set<ClassView>().Add(new ClassView(@event.Id, @event.ClassName));
        }
    }
}
