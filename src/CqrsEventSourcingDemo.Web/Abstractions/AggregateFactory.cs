using System;
using System.Collections.Generic;
using Reusables.EventSourcing;

namespace CqrsEventSourcingDemo.Web.Abstractions
{
    public class AggregateFactory : IAggregateFactory
    {
        public TAggregate Create<TAggregate>(IEnumerable<Event> history) where TAggregate : Aggregate
        {
            return (TAggregate) Activator.CreateInstance(typeof (TAggregate), history);
        }
    }
}
