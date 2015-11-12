using System;

namespace Reusables.EventSourcing
{
    public class EventData
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public string AggregateType { get; set; }

        public Guid AggregateId { get; set; }

        public long Version { get; set; }

        public string Event { get; set; }

        public string Metadata { get; set; }
    }
}
