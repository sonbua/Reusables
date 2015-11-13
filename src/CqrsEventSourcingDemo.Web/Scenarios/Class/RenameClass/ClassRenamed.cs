using System;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.RenameClass
{
    public class ClassRenamed : Event
    {
        public Guid Id { get; set; }

        public string NewName { get; set; }
    }
}
