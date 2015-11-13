using System.Collections.Generic;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions
{
    public interface IAggregateFactory
    {
        TAggregate Create<TAggregate>(IEnumerable<Event> history) where TAggregate : Aggregate;
    }
}
