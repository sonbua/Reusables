using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.RemoveClass
{
    public class ClassRemovedSubscriber : Subscriber<ClassRemoved>
    {
        private readonly IViewDatabase _viewDatabase;

        public ClassRemovedSubscriber(IViewDatabase viewDatabase)
        {
            _viewDatabase = viewDatabase;
        }

        public override void Handle(ClassRemoved @event)
        {
            _viewDatabase.Set<ClassView>().Remove(@event.Id);
        }
    }
}
