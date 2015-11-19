using MailingServiceDemo.Model;
using Reusables.Cqrs;

namespace MailingServiceDemo.Query
{
    public class MostUrgentMessage : Query<OutboxMessage>
    {
    }
}
