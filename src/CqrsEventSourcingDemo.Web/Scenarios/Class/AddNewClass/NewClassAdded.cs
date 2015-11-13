using System;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass
{
    public class NewClassAdded : Event
    {
        public Guid Id { get; set; }

        public string ClassName { get; set; }
    }
}
