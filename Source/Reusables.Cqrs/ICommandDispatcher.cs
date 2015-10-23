using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command) where TCommand : Command;

        Task DispatchAsync<TAsyncCommand>(TAsyncCommand command) where TAsyncCommand : AsyncCommand;
    }
}
