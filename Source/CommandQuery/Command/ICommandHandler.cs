﻿using System.Threading.Tasks;

namespace CommandQuery.Command
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        void Handle(TCommand command);
    }

    public interface IAsyncCommandHandler<TAsyncCommand> where TAsyncCommand : AsyncCommand
    {
        Task HandleAsync(TAsyncCommand command);
    }
}