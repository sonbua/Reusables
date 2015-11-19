using MailingServiceDemo.Model;

namespace MailingServiceDemo.Event
{
    public class DeliveryNeeded
    {
        public OutboxMessage Message { get; set; }
    }
}
