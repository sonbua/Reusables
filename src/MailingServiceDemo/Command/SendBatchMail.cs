using System;
using System.Net.Mail;

namespace MailingServiceDemo.Command
{
    public class SendBatchMail
    {
        public Guid Id { get; set; }

        public MailMessage[] Messages { get; set; }

        public int Priority { get; set; }
    }
}
