using System;
using MailingServiceDemo.Event;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class MailSender : IEventSubscriber<MessageQueued>
    {
        private readonly ISmtpClientWrapper _smtpClient;
        private readonly IEventPublisher _eventPublisher;

        private readonly ILogger _logger;

        public MailSender(ISmtpClientWrapper smtpClient, IEventPublisher eventPublisher, ILogger logger)
        {
            _smtpClient = smtpClient;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public void Handle(MessageQueued @event)
        {
            try
            {
                _smtpClient.Send(@event.Message);

                _eventPublisher.Publish(new MessageSent
                                        {
                                            MessageId = @event.MessageId,
                                            Message = @event.Message,
                                            SentAt = DateTime.UtcNow
                                        });
            }
            catch (Exception exception)
            {
                _logger.Error(exception);

                _eventPublisher.Publish(new SendingFailed
                                        {
                                            MessageId = @event.MessageId,
                                            Message = @event.Message,
                                            Reason = exception.Message,
                                            TriedAt = DateTime.UtcNow
                                        });
            }
        }
    }
}
