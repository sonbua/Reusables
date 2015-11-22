using System;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Event
{
    public class ManualAnalysisRequired
    {
        public Guid MessageId { get; set; }

        public MailMessage Message { get; set; }
    }
}
