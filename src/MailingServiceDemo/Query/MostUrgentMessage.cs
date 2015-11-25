using MailingServiceDemo.Model;
using Reusables;
using Reusables.Cqrs;

namespace MailingServiceDemo.Query
{
    public class MostUrgentMessage : Query<Optional<OngoingMessage>>
    {
    }
}
