using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    public interface IAggregateFactory
    {
        TAggregate Create<TAggregate>(IEnumerable<Event> history) where TAggregate : Aggregate;
    }
}
