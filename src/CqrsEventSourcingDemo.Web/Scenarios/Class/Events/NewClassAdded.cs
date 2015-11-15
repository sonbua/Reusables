using System;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.Events
{
    public class NewClassAdded
    {
        public Guid Id { get; set; }

        public string ClassName { get; set; }
    }
}
