using System;
using Reusables.Cqrs;
using Reusables.DataAnnotations;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass
{
    public class AddNewClassCommand : Command
    {
        public Guid ClassId { get; set; }

        [Required]
        public string ClassName { get; set; }
    }
}
