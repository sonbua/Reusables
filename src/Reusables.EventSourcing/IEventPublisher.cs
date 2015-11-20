using System.Threading.Tasks;

namespace Reusables.EventSourcing
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);

        Task PublishAsync<TEvent>(TEvent @event);
    }
}
