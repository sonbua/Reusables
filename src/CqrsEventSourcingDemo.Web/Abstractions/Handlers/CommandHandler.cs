using System;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions.Handlers
{
    public abstract class CommandHandler<TAggregate, TCommand> : ICommandHandler<TCommand>
        where TAggregate : Aggregate, new()
        where TCommand : Command
    {
        private readonly IRepository _repository;

        protected CommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public abstract void Handle(TCommand command);

        protected virtual void Act(Guid id, Action<TAggregate> action)
        {
            var aggregate = _repository.GetById<TAggregate>(id);

            action(aggregate);

            _repository.Save(aggregate);
        }
    }
}
