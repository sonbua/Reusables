using System;
using System.Net.Mail;

namespace MailingServiceDemo.Command
{
    public class SendMail
    {
        public Guid Id { get; set; }

        public MailMessage[] Messages { get; set; }
    }
}
