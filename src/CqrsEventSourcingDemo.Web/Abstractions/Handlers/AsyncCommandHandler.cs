using System;
using System.Threading.Tasks;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions.Handlers
{
    public abstract class AsyncCommandHandler<TAggregate, TAsyncCommand> : IAsyncCommandHandler<TAsyncCommand>
        where TAggregate : Aggregate, new()
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
