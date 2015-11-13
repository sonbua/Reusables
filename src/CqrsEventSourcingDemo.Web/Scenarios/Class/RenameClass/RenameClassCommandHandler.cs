using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.RenameClass
{
    public class RenameClassCommandHandler : CommandHandler<ClassAggregate, RenameClassCommand>
    {
        public RenameClassCommandHandler(IRepository repository) : base(repository)
        {
        }

        public override void Handle(RenameClassCommand command)
        {
            Act(command.Id, aggregate => aggregate.Rename(command.Id, command.NewName));
        }
    }
}
