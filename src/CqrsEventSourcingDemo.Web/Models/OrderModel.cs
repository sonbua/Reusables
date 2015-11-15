using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Web.Models
{
    public class OrderModel
    {
        public List<OrderLineItem> Items { get; set; }
    }

    public class OrderLineItem
    {
        public int MenuNumber { get; set; }

        public string Description { get; set; }

        public int NumberToOrder { get; set; }
    }
}
