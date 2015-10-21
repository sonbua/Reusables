using System.Threading.Tasks;

namespace CommandQuery.Command
{
    public interface IAsyncCommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : Command;
    }
}