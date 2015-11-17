using System;
using System.Collections.Generic;
using Reusables.EventSourcing;

namespace MailingServiceDemo
{
    public class AggregateFactory : IAggregateFactory
    {
        public TAggregate Create<TAggregate>(IEnumerable<object> history) where TAggregate : Reusables.EventSourcing.Aggregate
        {
            return (TAggregate) Activator.CreateInstance(typeof (TAggregate), history);
        }
    }
}
