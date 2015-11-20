using System;
using System.Net.Mail;

namespace MailingServiceDemo.Event
{
    public class MailRequestAccepted
    {
        public Guid Id { get; set; }

        public MailMessage[] Messages { get; set; }
    }
}
