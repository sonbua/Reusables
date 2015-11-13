using System;
using System.Collections.Generic;
using CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass;
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
}
