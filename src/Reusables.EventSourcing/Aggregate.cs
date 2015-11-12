using System;
using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    public abstract class Aggregate
    {
        protected List<object> UncommittedEvents;

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
