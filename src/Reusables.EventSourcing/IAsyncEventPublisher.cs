using System.Threading.Tasks;

namespace Reusables.EventSourcing
{
    public interface IAsyncEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event);
    }
}
