using Reusables.Cqrs;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class GetTodoLists : Query<TodoList[]>
    {
    }
}
