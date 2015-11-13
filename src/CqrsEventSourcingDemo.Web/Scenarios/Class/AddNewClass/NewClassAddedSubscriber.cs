using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass
{
    public class NewClassAddedSubscriber : Subscriber<NewClassAdded>
    {
        private readonly IViewDatabase _viewDatabase;

        public NewClassAddedSubscriber(IViewDatabase viewDatabase)
        {
            _viewDatabase = viewDatabase;
        }

        public override void Handle(NewClassAdded @event)
        {
            _viewDatabase.Set<ClassView>().Add(new ClassView(@event.Id, @event.ClassName));
        }
    }
}
