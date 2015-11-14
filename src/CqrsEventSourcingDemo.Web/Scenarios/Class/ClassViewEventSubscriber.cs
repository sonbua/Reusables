using CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass;
using CqrsEventSourcingDemo.Web.Scenarios.Class.RemoveClass;
using CqrsEventSourcingDemo.Web.Scenarios.Class.RenameClass;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class
{
    public class ClassViewEventSubscriber : IEventSubscriber<NewClassAdded>,
                                            IEventSubscriber<ClassRenamed>,
                                            IEventSubscriber<ClassRemoved>
    {
        private readonly IViewModelDatabase _database;

        public ClassViewEventSubscriber(IViewModelDatabase database)
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
