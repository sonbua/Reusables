using System;
using System.Collections.Generic;
using CqrsEventSourcingDemo.Web.Domain.Events.Tab;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace CqrsEventSourcingDemo.Web.Domain.Commands.Tab
{
    public class TabAggregate : Aggregate
    {
        private bool _open;

        public TabAggregate(IEnumerable<object> history)
        {
            foreach (var @event in history)
            {
                Apply(@event);
            }
        }

        public void OpenTab(Guid id, int tableNumber, string waiter)
        {
            Publish(new TabOpened
                    {
                        Id = id,
                        TableNumber = tableNumber,
                        Waiter = waiter
                    });
        }

        private void Publish<TEvent>(TEvent @event)
        {
            UncommittedEvents.Add(@event);

            Apply(@event);
        }

        private void Apply<TEvent>(TEvent @event)
        {
            Version++;

            this.Replay(@event);
        }

        private void When(TabOpened @event)
        {
            _open = true;
        }
    }
}
