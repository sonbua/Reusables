using System;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.RemoveClass
{
    public class ClassRemoved : Event
    {
        public Guid Id { get; set; }
    }
}
