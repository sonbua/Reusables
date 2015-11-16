using System;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class TabIdForTable : Query<Guid>
    {
        public int TableNumber { get; set; }
    }
}
