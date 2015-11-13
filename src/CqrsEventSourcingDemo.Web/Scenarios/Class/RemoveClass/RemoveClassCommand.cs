using System;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.RemoveClass
{
    public class RemoveClassCommand : Command
    {
        public Guid Id { get; set; }
    }
}
