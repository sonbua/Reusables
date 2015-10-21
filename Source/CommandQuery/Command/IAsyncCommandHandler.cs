using System.Threading.Tasks;

namespace CommandQuery.Command
{
    public interface IAsyncCommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}