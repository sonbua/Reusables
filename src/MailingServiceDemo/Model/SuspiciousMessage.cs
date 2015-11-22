using System;

namespace MailingServiceDemo.Model
{
    public class SuspiciousMessage : Entity
    {
        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }
    }
}
