using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.RenameClass
{
    public class ClassRenamedSubscriber : Subscriber<ClassRenamed>
    {
        private readonly IViewDatabase _viewDatabase;

        public ClassRenamedSubscriber(IViewDatabase viewDatabase)
        {
            _viewDatabase = viewDatabase;
        }

        public override void Handle(ClassRenamed @event)
        {
            var view = _viewDatabase.Set<ClassView>().GetById(@event.Id);

            view.Name = @event.NewName;
        }
    }
}
