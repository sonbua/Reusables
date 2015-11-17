using System;
using System.Net.Mail;

namespace MailingServiceDemo.ReadModel
{
    public class OutboxMessage : ViewModel
    {
        public Guid RequestId { get; set; }

        public MailMessage MailMessage { get; set; }
    }
}
