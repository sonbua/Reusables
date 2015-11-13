using System;
using Reusables.Cqrs;
using Reusables.DataAnnotations;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass
{
    public class AddNewClassCommand : Command
    {
        public Guid ClassId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ClassName { get; set; }
    }
}
