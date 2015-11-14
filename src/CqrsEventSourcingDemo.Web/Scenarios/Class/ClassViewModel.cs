using System;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class
{
    public class ClassViewModel : ViewModel
    {
        public ClassViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
    }
}
