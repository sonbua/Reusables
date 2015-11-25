using System;
using System.Linq;
using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using MailingServiceDemo.Query;
using Reusables;
using Reusables.Cqrs;

namespace MailingServiceDemo.QueryHandler
{
    public class StoreKeeper : IQueryHandler<MostUrgentMessage, Optional<OngoingMessage>>
    {
        private readonly IDbContext _dbContext;

        public StoreKeeper(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Optional<OngoingMessage> Handle(MostUrgentMessage query)
        {
            var result = _dbContext.Set<OutboxMessage>()
                                   .OrderByDescending(message => message.Priority)
                                   .ThenBy(message => message.QueuedAt)
                                   .Select(message => new OngoingMessage {Id = message.Id, RequestId = message.RequestId, Message = message.Message, Priority = message.Priority, QueuedAt = DateTime.UtcNow})
                                   .FirstOrDefault();

            if (result != null)
            {
                _dbContext.Set<OngoingMessage>().Add(result);
                _dbContext.Set<OutboxMessage>().Remove(result.Id);
            }

            return result;
        }
    }
}
