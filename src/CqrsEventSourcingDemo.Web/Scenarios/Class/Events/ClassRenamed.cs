using System;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.Events
{
    public class ClassRenamed
    {
        public Guid Id { get; set; }

        public string NewName { get; set; }
    }
}
