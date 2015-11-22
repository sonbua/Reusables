using System;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Event
{
    public class MessageSent
    {
        public Guid RequestId { get; set; }

        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }

        public DateTime SentAt { get; set; }
    }
}
