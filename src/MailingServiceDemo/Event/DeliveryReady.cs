using MailingServiceDemo.Model;

namespace MailingServiceDemo.Event
{
    public class DeliveryReady
    {
        public OngoingMessage Message { get; set; }
    }
}
