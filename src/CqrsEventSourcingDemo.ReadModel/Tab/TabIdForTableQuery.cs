using System;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class TabIdForTableQuery : Query<Guid>
    {
        public int TableNumber { get; set; }
    }
}
