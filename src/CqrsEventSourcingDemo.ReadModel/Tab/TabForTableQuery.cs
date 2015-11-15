using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class TabForTableQuery : Query<TabStatus>
    {
        public int TableNumber { get; set; }
    }
}
