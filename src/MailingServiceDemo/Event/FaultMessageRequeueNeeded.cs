using System;

namespace MailingServiceDemo.Event
{
    public class FaultMessageRequeueNeeded
    {
        public Guid MessageId { get; set; }
    }
}
