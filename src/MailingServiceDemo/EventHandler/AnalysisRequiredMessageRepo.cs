using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using MailingServiceDemo.ReadModel;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class AnalysisRequiredMessageRepo : IEventSubscriber<AnalysisRequired>
    {
        private readonly IViewModelDatabase _database;

        public AnalysisRequiredMessageRepo(IViewModelDatabase database)
        {
            _database = database;
        }

        public void Handle(AnalysisRequired @event)
        {
            _database.Set<AnalysisRequiredMessage>()
                     .Add(new AnalysisRequiredMessage
                          {
                              MessageId = @event.MessageId,
                              Message = @event.Message
                          });
        }
    }
}
