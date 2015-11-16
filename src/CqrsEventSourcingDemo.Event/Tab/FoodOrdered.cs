using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Event.Tab
{
    public class FoodOrdered
    {
        public FoodOrdered()
        {
            Items = new List<OrderedItem>();
        }

        public Guid TabId { get; set; }

        public List<OrderedItem> Items { get; set; }
    }
}
