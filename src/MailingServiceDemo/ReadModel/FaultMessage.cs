using System;
using System.Net.Mail;

namespace MailingServiceDemo.ReadModel
{
    public class FaultMessage : ViewModel
    {
        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }

        public string Reason { get; set; }

        public DateTime TriedAt { get; set; }
    }
}
