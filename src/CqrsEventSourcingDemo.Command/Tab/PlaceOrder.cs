using System;
using System.Collections.Generic;
using CqrsEventSourcingDemo.Event.Tab;

namespace CqrsEventSourcingDemo.Command.Tab
{
    public class PlaceOrder
    {
        public PlaceOrder()
        {
            Items = new List<OrderedItem>();
        }

        public Guid TabId { get; set; }

        public List<OrderedItem> Items { get; set; }
    }
}
