using System;

namespace CqrsEventSourcingDemo.Event.Tab
{
    public class TabClosed
    {
        public Guid TabId { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal OrderValue { get; set; }

        public decimal TipValue { get; set; }
    }
}