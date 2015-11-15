using System.Collections.Generic;

namespace Reusables.EventSourcing
{
    public interface IAggregateFactory
    {
        TAggregate Create<TAggregate>(IEnumerable<object> history) where TAggregate : Aggregate;
    }
}
