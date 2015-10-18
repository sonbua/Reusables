namespace CommandQuery.Command
{
    public interface ICommandHandler
    {
        void Handle(object command);
    }

    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : Command
    {
        void Handle(TCommand command);
    }
}