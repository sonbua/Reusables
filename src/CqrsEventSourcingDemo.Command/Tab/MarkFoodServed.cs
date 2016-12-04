using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Command.Tab
{
    public class MarkFoodServed
    {
        public Guid TabId { get; set; }

        public List<int> MenuNumbers { get; set; }
    }
}
