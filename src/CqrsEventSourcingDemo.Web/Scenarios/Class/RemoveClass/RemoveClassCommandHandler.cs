using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.RemoveClass
{
    public class RemoveClassCommandHandler : CommandHandler<ClassAggregate, RemoveClassCommand>
    {
        public RemoveClassCommandHandler(IRepository repository) : base(repository)
        {
        }

        public override void Handle(RemoveClassCommand command)
        {
            Act(command.Id, aggregate => aggregate.Remove(command.Id));
        }
    }
}
