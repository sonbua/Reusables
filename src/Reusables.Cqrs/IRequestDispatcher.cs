namespace Reusables.Cqrs
{
    /// <summary>
    /// Defines an interface for a request dispatcher (of both command type and query type). This dispatcher is responsible for distributing a command/query object to the right command/query handler, letting it process the command/query, then returning result back to the caller (if any).
    /// </summary>
    public interface IRequestDispatcher : ICommandDispatcher, IQueryDispatcher
    {
    }
}
