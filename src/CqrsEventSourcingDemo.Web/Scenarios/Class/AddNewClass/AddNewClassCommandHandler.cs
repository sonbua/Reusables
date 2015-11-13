using System;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.AddNewClass
{
    public class AddNewClassCommandHandler : CommandHandler<ClassAggregate, AddNewClassCommand>
    {
        public AddNewClassCommandHandler(IRepository repository) : base(repository)
        {
        }

        public override void Handle(AddNewClassCommand command)
        {
            var id = Guid.NewGuid();

            Act(id, aggregate => aggregate.AddNew(id, command.ClassName));

            command.ClassId = id;
        }
    }
}
