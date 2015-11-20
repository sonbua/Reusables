using System;
using MailingServiceDemo.Command;
using MailingServiceDemo.Event;
using Reusables.Cqrs;
using Reusables.EventSourcing;

namespace MailingServiceDemo.CommandHandler
{
    public class PostOffice : ICommandHandler<SendMail>
    {
        private readonly IEventPublisher _eventPublisher;

        public PostOffice(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(SendMail command)
        {
            var id = Guid.NewGuid();

            _eventPublisher.Publish(new MailRequestAccepted
                                    {
                                        Id = id,
                                        Messages = command.Messages,
                                    });

            command.Id = id;
        }
    }
}
