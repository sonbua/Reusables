using System;
using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    public abstract class Aggregate
    {
        protected List<Event> UncommittedEvents = new List<Event>();

        public Guid Id { get; protected set; }

        public long Version { get; protected set; }

        public Event[] GetUncommittedEvents()
        {
            return UncommittedEvents.ToArray();
        }

        public void ClearUncommittedEvents()
        {
            UncommittedEvents.Clear();
        }
    }
}
