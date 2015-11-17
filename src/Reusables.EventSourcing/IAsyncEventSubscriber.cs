using System.Threading.Tasks;

namespace Reusables.EventSourcing
{
    public interface IAsyncEventSubscriber<TEvent>
    {
        Task HandleAsync(TEvent @event);
    }
}
