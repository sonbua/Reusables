using System;

namespace MailingServiceDemo.Model
{
    public class OutboxMessage : Entity
    {
        public Guid RequestId { get; set; }

        public MailMessage Message { get; set; }

        public int Priority { get; set; }

        public DateTime QueuedAt { get; set; }
    }
}
