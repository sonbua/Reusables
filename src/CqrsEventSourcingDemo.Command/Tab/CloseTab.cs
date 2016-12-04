using System;

namespace CqrsEventSourcingDemo.Command.Tab
{
    public class CloseTab
    {
        public Guid TabId { get; set; }

        public decimal AmountPaid { get; set; }
    }
}