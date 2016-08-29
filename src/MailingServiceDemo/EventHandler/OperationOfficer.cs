using System.Threading.Tasks;
using MailingServiceDemo.Event;
using MailingServiceDemo.Query;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class OperationOfficer : IEventSubscriber<OutboxManagementNeeded>,
                                    IAsyncEventSubscriber<OutboxManagementNeeded>
    {
        private readonly IRequestDispatcher _dispatcher;
        private readonly IEventPublisher _eventPublisher;

        public OperationOfficer(IRequestDispatcher dispatcher, IEventPublisher eventPublisher)
        {
            _dispatcher = dispatcher;
            _eventPublisher = eventPublisher;
        }

        public void Handle(OutboxManagementNeeded @event)
        {
            var urgentMessage = _dispatcher.DispatchQuery(new MostUrgentMessage());

            if (urgentMessage.HasValue)
                _eventPublisher.Publish(new DeliveryReady {Message = urgentMessage.Value});
        }

        public async Task HandleAsync(OutboxManagementNeeded @event)
        {
            var urgentMessage = _dispatcher.DispatchQuery(new MostUrgentMessage());

            if (urgentMessage.HasValue)
                await _eventPublisher.PublishAsync(new DeliveryReady {Message = urgentMessage.Value}).ConfigureAwait(false);
        }
    }
}
