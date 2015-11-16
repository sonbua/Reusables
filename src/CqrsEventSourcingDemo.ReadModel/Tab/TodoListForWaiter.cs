using System.Collections.Generic;
using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class TodoListForWaiter : Query<IDictionary<int, TabItem[]>>
    {
        public string StaffId { get; set; }
    }
}
