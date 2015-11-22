using System;

namespace MailingServiceDemo.Model
{
    public class FaultMessage : Entity
    {
        public Guid RequestId { get; set; }

        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }

        public string Reason { get; set; }

        public DateTime TriedAt { get; set; }
    }
}
