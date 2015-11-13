using System;
using System.Collections.Generic;
using CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass;
using CqrsEventSourcingDemo.Web.Scenarios.Class.RenameClass;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class
{
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

        public void Rename(Guid id, string newName)
        {
            Publish(new ClassRenamed {Id = id, NewName = newName});
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

        private void When(ClassRenamed @event)
        {
            Name = @event.NewName;
        }
    }
}
