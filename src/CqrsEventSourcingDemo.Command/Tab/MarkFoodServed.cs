using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Command.Tab
{
    public class MarkFoodServed
    {
        public Guid Id { get; set; }

        public List<int> MenuNumbers { get; set; }
    }
}
