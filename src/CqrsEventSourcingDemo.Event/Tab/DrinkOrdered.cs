using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Event.Tab
{
    public class DrinkOrdered
    {
        public DrinkOrdered()
        {
            Items = new List<OrderedItem>();
        }

        public Guid TabId { get; set; }

        public List<OrderedItem> Items { get; set; }
    }
}
