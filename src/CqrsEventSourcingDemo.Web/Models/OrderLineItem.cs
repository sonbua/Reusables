namespace CqrsEventSourcingDemo.Web.Models
{
    public class OrderLineItem
    {
        public int MenuNumber { get; set; }

        public string Description { get; set; }

        public int NumberToOrder { get; set; }
    }
}