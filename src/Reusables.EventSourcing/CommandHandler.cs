using System;
using System.Threading.Tasks;
using Reusables.Cqrs;

namespace Reusables.EventSourcing
{
    public abstract class CommandHandler<TAggregate, TCommand> : ICommandHandler<TCommand>
        where TAggregate : Aggregate
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

    public abstract class AsyncCommandHandler<TAggregate, TAsyncCommand> : IAsyncCommandHandler<TAsyncCommand>
        where TAggregate : Aggregate
        where TAsyncCommand : AsyncCommand
    {
        private readonly IRepository _repository;

        protected AsyncCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public abstract Task HandleAsync(TAsyncCommand command);

        protected virtual async Task Act(Guid id, Action<TAggregate> action)
        {
            var aggregate = await _repository.GetByIdAsync<TAggregate>(id);

            action(aggregate);

            await _repository.SaveAsync(aggregate);
        }
    }
}
