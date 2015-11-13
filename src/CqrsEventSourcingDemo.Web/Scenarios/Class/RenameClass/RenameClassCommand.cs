using System;
using Reusables.Cqrs;
using Reusables.DataAnnotations;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.RenameClass
{
    public class RenameClassCommand : Command
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string NewName { get; set; }
    }
}
