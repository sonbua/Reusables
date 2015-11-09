using System;

namespace Reusables.EventSourcing
{
    public abstract class Aggregate
    {
        public abstract Guid Id { get; }

        public long Version { get; set; }
    }
}
