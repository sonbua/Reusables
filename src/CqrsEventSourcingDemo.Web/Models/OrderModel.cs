using System.Collections.Generic;

namespace CqrsEventSourcingDemo.Web.Models
{
    public class OrderModel
    {
        public List<OrderLineItem> Items { get; set; }
    }
}