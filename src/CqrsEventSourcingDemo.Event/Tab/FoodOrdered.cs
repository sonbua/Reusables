using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Event.Tab
{
    public class FoodOrdered
    {
        public Guid TabId { get; set; }

        public List<OrderedItem> Items { get; set; }
    }
}
