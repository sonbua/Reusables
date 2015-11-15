using System;
using Reusables.Cqrs;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace CqrsEventSourcingDemo.Web.Scenarios.Class.Commands
{
    public class ClassCommandHandlers : ICommandHandler<AddNewClass>,
                                        ICommandHandler<RenameClass>,
                                        ICommandHandler<RemoveClass>
    {
        private readonly IEventStore _eventStore;

        public ClassCommandHandlers(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Handle(AddNewClass command)
        {
            // TODO: use COMB GUID technique
            var id = Guid.NewGuid();

            _eventStore.Act<ClassAggregate>(id, aggregate => aggregate.AddNew(id, command.ClassName));

            command.ClassId = id;
        }

        public void Handle(RenameClass command)
        {
            _eventStore.Act<ClassAggregate>(command.Id, aggregate => aggregate.Rename(command.Id, command.NewName));
        }

        public void Handle(RemoveClass command)
        {
            _eventStore.Act<ClassAggregate>(command.Id, aggregate => aggregate.Remove(command.Id));
        }
    }
}
