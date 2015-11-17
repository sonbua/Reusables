using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using Reusables.EventSourcing;

namespace MailingServiceDemo.ReadModel
{
    public class SentMessageReadModel : IEventSubscriber<MessageSent>
    {
        private readonly IViewModelDatabase _database;

        public SentMessageReadModel(IViewModelDatabase database)
        {
            _database = database;
        }

        public void Handle(MessageSent @event)
        {
            _database.Set<SentMessage>().Add(new SentMessage
                                             {
                                                 MessageId = @event.MessageId,
                                                 Message = @event.Message,
                                                 SentAt = @event.SentAt
                                             });
        }
    }
}
