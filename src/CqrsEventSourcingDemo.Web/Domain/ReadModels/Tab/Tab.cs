using System.Collections.Generic;
using CqrsEventSourcingDemo.Web.Abstractions.Views;

namespace CqrsEventSourcingDemo.Web.Domain.ReadModels.Tab
{
    public class Tab : ViewModel
    {
        public Tab()
        {
            ToServe = new List<TabItem>();
            InPreparation = new List<TabItem>();
            Served = new List<TabItem>();
        }

        public int TableNumber { get; set; }

        public string Waiter { get; set; }

        public List<TabItem> ToServe { get; set; }

        public List<TabItem> InPreparation { get; set; }

        public List<TabItem> Served { get; set; }
    }
}
