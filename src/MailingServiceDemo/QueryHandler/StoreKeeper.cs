using System.Linq;
using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using MailingServiceDemo.Query;
using Reusables.Cqrs;

namespace MailingServiceDemo.QueryHandler
{
    public class StoreKeeper : IQueryHandler<MostUrgentMessage, OutboxMessage>
    {
        private readonly IDbContext _dbContext;

        public StoreKeeper(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OutboxMessage Handle(MostUrgentMessage query)
        {
            return _dbContext.Set<OutboxMessage>()
                             .OrderByDescending(message => message.Priority)
                             .ThenBy(message => message.QueuedAt)
                             .First();
        }
    }
}
