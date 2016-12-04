using System;
using System.Linq;
using CqrsEventSourcingDemo.Event.Tab;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.ReadModel.Tab
{
    public class ChefTodoListReadModel :
        IQueryHandler<GetTodoLists, TodoList[]>,
        IEventSubscriber<FoodOrdered>,
        IEventSubscriber<FoodPrepared>
    {
        private readonly IViewModelDatabase _database;

        public ChefTodoListReadModel(IViewModelDatabase database)
        {
            _database = database;
        }

        public TodoList[] Handle(GetTodoLists query)
        {
            return _database.Set<TodoList>().Where(list => list.Items.Any()).ToArray();
        }

        public void Handle(FoodOrdered @event)
        {
            var todoListByTab = _database.Set<TodoList>().FirstOrDefault(@group => @group.TabId == @event.TabId);
            var todoItems = @event.Items.Select(item => (TodoItem) item);

            if (todoListByTab != null)
            {
                todoListByTab.Items.AddRange(todoItems);
                return;
            }

            _database.Set<TodoList>()
                .Add(new TodoList
                {
                    Id = Guid.NewGuid(),
                    TabId = @event.TabId,
                    Items = todoItems.ToList()
                });
        }

        public void Handle(FoodPrepared @event)
        {
            var todoListByTab = _database.Set<TodoList>().First(x => x.TabId == @event.TabId);

            foreach (var menuNumber in @event.MenuNumbers)
            {
                var food = todoListByTab.Items.First(item => item.MenuNumber == menuNumber);

                todoListByTab.Items.Remove(food);
            }
        }
    }
}