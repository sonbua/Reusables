namespace CqrsEventSourcingDemo.Web.Controllers.Attributes
{
    internal class MenuItem
    {
        public int MenuNumber { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsDrink { get; set; }
    }
}