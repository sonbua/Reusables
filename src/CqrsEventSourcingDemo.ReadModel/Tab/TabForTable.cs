using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class TabForTable : Query<TabStatus>
    {
        public int TableNumber { get; set; }
    }
}
