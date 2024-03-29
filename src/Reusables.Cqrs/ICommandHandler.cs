﻿using System.Threading.Tasks;

namespace Reusables.Cqrs
{
    /// <summary>
    /// Defines a common interface for synchronous command handlers.
    /// </summary>
    /// <typeparam name="TCommand">The type of command.</typeparam>
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }

    /// <summary>
    /// Defines a common interface for asynchronous command handlers.
    /// </summary>
    /// <typeparam name="TCommand">The type of asynchronous command.</typeparam>
    public interface IAsyncCommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
