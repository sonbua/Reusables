using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using MailingServiceDemo.ReadModel;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class SentMessageRepo : IEventSubscriber<MessageSent>
    {
        private readonly IViewModelDatabase _database;

        public SentMessageRepo(IViewModelDatabase database)
        {
            _database = database;
        }

        public void Handle(MessageSent @event)
        {
            _database.Set<SentMessage>().Add(new SentMessage
                                             {
                                                 Id = @event.MessageId,
                                                 Message = @event.Message,
                                                 SentAt = @event.SentAt
                                             });
        }
    }
}
