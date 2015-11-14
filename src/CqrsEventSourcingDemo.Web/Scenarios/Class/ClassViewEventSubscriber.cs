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
        private readonly IViewDatabase _viewDatabase;

        public ClassViewEventSubscriber(IViewDatabase viewDatabase)
        {
            _viewDatabase = viewDatabase;
        }

        public void Handle(NewClassAdded @event)
        {
            _viewDatabase.Set<ClassView>().Add(new ClassView(@event.Id, @event.ClassName));
        }

        public void Handle(ClassRenamed @event)
        {
            var view = _viewDatabase.Set<ClassView>().GetById(@event.Id);

            view.Name = @event.NewName;
        }

        public void Handle(ClassRemoved @event)
        {
            _viewDatabase.Set<ClassView>().Remove(@event.Id);
        }
    }
}
