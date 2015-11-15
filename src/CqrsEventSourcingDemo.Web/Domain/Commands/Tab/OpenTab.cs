using System;
using System.ComponentModel.DataAnnotations;

namespace CqrsEventSourcingDemo.Web.Domain.Commands.Tab
{
    public class OpenTab
    {
        public Guid Id { get; set; }

        public string Waiter { get; set; }

        [Display(Name = "Table Number")]
        public int TableNumber { get; set; }
    }
}
