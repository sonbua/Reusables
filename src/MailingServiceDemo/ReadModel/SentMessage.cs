using System;
using System.Net.Mail;

namespace MailingServiceDemo.ReadModel
{
    public class SentMessage : ViewModel
    {
        public MailMessage Message { get; set; }

        public DateTime SentAt { get; set; }
    }
}
