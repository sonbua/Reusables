using System;
using System.Net.Mail;

namespace MailingServiceDemo.Event
{
    public class MessageSent
    {
        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }

        public DateTime SentAt { get; set; }
    }
}
