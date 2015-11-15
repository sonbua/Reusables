using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.Events
{
    public class ClassEventSubscribers : IEventSubscriber<NewClassAdded>,
                                         IEventSubscriber<ClassRenamed>,
                                         IEventSubscriber<ClassRemoved>
    {
        private readonly IViewModelDatabase _database;

        public ClassEventSubscribers(IViewModelDatabase database)
        {
            _database = database;
        }

        public void Handle(NewClassAdded @event)
        {
            _database.Set<ClassViewModel>().Add(new ClassViewModel(@event.Id, @event.ClassName));
        }

        public void Handle(ClassRenamed @event)
        {
            var viewModel = _database.Set<ClassViewModel>().GetById(@event.Id);

            viewModel.Name = @event.NewName;
        }

        public void Handle(ClassRemoved @event)
        {
            _database.Set<ClassViewModel>().Remove(@event.Id);
        }
    }
}
