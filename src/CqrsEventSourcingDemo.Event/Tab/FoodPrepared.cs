using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Event.Tab
{
    public class FoodPrepared
    {
        public Guid TabId { get; set; }

        public List<int> MenuNumbers { get; set; }
    }
}