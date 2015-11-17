using System;
using System.Net.Mail;

namespace MailingServiceDemo.Event
{
    public class MessageQueued
    {
        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }
    }
}
