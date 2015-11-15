using System;
using Reusables.DataAnnotations;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.Commands
{
    public class AddNewClass
    {
        public Guid ClassId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ClassName { get; set; }
    }
}
