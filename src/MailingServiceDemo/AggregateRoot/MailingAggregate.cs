using System;
using System.Collections.Generic;
using System.Net.Mail;
using MailingServiceDemo.Event;
using Reusables.EventSourcing;
using Reusables.EventSourcing.Extensions;

namespace MailingServiceDemo.AggregateRoot
{
    public class MailingAggregate : Aggregate
    {
        public MailingAggregate(IEnumerable<object> history)
        {
            foreach (var @event in history)
            {
                Apply(@event);
            }
        }

        public void SendMail(Guid id, MailMessage[] messages, int priority)
        {
            Publish(new MailRequestReceived
                    {
                        Id = id,
                        Messages = messages,
                        Priority = priority
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

            this.ApplyEventOptionally(@event);
        }

        private void When(MailRequestReceived @event)
        {
            Id = @event.Id;
        }
    }
}
