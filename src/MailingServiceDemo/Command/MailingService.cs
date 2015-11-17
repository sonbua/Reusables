using System;
using MailingServiceDemo.Event;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace MailingServiceDemo.Command
{
    public class MailingService : ICommandHandler<SendMail>
    {
        private readonly IEventPublisher _eventPublisher;

        public MailingService(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(SendMail command)
        {
            var id = Guid.NewGuid();

            _eventPublisher.Publish(new MailRequestReceived
                                    {
                                        Id = id,
                                        Messages = command.Messages,
                                        Priority = command.Priority
                                    });

            command.Id = id;
        }
    }
}
