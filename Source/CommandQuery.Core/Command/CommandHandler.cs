namespace CommandQuery.Core.Command
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : Command
    {
        public abstract void Handle(TCommand command);

        public void Handle(object command)
        {
            Handle((TCommand) command);
        }
    }
}