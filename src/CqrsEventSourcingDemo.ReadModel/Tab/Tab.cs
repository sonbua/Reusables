using System;
using System.Collections.Generic;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class Tab : ViewModel
    {
        public Tab()
        {
            Status = TabStatuses.Open;
            ToServe = new List<TabItem>();
            InPreparation = new List<TabItem>();
            Served = new List<TabItem>();
        }

        public int TableNumber { get; set; }

        public string Waiter { get; set; }

        public List<TabItem> ToServe { get; set; }

        public List<TabItem> InPreparation { get; set; }

        public List<TabItem> Served { get; set; }

        public TabStatuses Status { get; set; }
    }

    public class TabItem
    {
        public int MenuNumber { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }

    public class TabStatus
    {
        public Guid TabId { get; set; }

        public int TableNumber { get; set; }

        public List<TabItem> ToServe { get; set; }

        public List<TabItem> InPreparation { get; set; }

        public List<TabItem> Served { get; set; }
    }

    public enum TabStatuses
    {
        Open = 0,
        Closed = 1
    }
}
