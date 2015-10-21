using System.Threading.Tasks;

namespace CommandQuery.Command
{
    public abstract class AsyncCommandHandler<TCommand> : IAsyncCommandHandler<TCommand> where TCommand : Command
    {
        public abstract Task HandleAsync(TCommand command);
    }
}