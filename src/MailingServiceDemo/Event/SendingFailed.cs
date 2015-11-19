using System;
using System.Net.Mail;

namespace MailingServiceDemo.Event
{
    public class SendingFailed
    {
        public Guid RequestId { get; set; }

        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }

        public string Reason { get; set; }

        public DateTime TriedAt { get; set; }
    }
}
