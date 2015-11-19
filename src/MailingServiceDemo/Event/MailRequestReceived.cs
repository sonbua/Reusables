using System;
using System.Net.Mail;

namespace MailingServiceDemo.Event
{
    public class MailRequestReceived
    {
        public Guid Id { get; set; }

        public MailMessage[] Messages { get; set; }
    }
}
