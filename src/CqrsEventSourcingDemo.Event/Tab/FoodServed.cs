﻿using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Event.Tab
{
    public class FoodServed
    {
        public Guid TabId { get; set; }

        public List<int> MenuNumbers { get; set; }
    }
}