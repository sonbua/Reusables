using System;
using Reusables.DataAnnotations;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.Commands
{
    public class RenameClass
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string NewName { get; set; }
    }
}
