using System;
using System.Net.Mail;

namespace MailingServiceDemo.ReadModel
{
    public class OutboxMessage : ViewModel
    {
        public Guid RequestId { get; set; }

        public MailMessage Message { get; set; }

        public int Priority { get; set; }

        public DateTime QueuedAt { get; set; }
    }
}
