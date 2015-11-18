using System.Linq;
using MailingServiceDemo.Database;
using MailingServiceDemo.Query;
using MailingServiceDemo.ReadModel;
using Reusables.Cqrs;

namespace MailingServiceDemo.QueryHandler
{
    public class PostOffice : IQueryHandler<MostUrgentMessage, OutboxMessage>
    {
        private readonly IViewModelDatabase _database;

        public PostOffice(IViewModelDatabase database)
        {
            _database = database;
        }

        public OutboxMessage Handle(MostUrgentMessage query)
        {
            return _database.Set<OutboxMessage>()
                            .OrderByDescending(message => message.Priority)
                            .ThenBy(message => message.QueuedAt)
                            .First();
        }
    }
}
