using System;
using System.Net.Mail;

namespace MailingServiceDemo.Model
{
    public class SuspiciousMessage : Entity
    {
        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }
    }
}
