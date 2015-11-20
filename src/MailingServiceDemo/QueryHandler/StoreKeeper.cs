using System.Linq;
using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using MailingServiceDemo.Query;
using Reusables;
using Reusables.Cqrs;

namespace MailingServiceDemo.QueryHandler
{
    public class StoreKeeper : IQueryHandler<MostUrgentMessage, Optional<OutboxMessage>>
    {
        private readonly IDbContext _dbContext;

        public StoreKeeper(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Optional<OutboxMessage> Handle(MostUrgentMessage query)
        {
            return _dbContext.Set<OutboxMessage>()
                             .OrderByDescending(message => message.Priority)
                             .ThenBy(message => message.QueuedAt)
                             .FirstOrDefault();
        }
    }
}
