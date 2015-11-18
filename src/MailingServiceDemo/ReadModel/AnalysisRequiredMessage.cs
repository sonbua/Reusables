using System;
using System.Net.Mail;

namespace MailingServiceDemo.ReadModel
{
    public class AnalysisRequiredMessage : ViewModel
    {
        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }
    }
}
