using MailingServiceDemo.Event;
using MailingServiceDemo.Query;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class DeliveryManager : IEventSubscriber<OutboxManagementNeeded>
    {
        private readonly IRequestDispatcher _dispatcher;
        private readonly IEventPublisher _eventPublisher;

        public DeliveryManager(IRequestDispatcher dispatcher, IEventPublisher eventPublisher)
        {
            _dispatcher = dispatcher;
            _eventPublisher = eventPublisher;
        }

        public void Handle(OutboxManagementNeeded @event)
        {
            var urgentMessage = _dispatcher.DispatchQuery(new MostUrgentMessage());

            _eventPublisher.Publish(new DeliveryNeeded {Message = urgentMessage});
        }
    }
}
