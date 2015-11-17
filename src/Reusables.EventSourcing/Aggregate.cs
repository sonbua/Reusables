using System;
using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    // TODO: consider enforcing an empty constructor and a LoadFromHistory method to load aggregate from event store
    public abstract class Aggregate
    {
        protected List<object> UncommittedEvents;

        protected Aggregate()
        {
            UncommittedEvents = new List<object>();
        }

        public Guid Id { get; protected set; }

        public long Version { get; protected set; }

        public object[] GetUncommittedEvents()
        {
            return UncommittedEvents.ToArray();
        }

        public void ClearUncommittedEvents()
        {
            UncommittedEvents.Clear();
        }
    }
}
