using System.Threading.Tasks;

namespace CommandQuery.Command
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command) where TCommand : Command;

        Task DispatchAsync<TAsyncCommand>(TAsyncCommand command) where TAsyncCommand : AsyncCommand;
    }
}