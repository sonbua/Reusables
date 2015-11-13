using System;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class
{
    public class ClassView : View
    {
        public ClassView(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
    }
}
