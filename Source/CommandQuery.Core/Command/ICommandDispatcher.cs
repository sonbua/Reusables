namespace CommandQuery.Core.Command
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command) where TCommand : Command;
    }
}