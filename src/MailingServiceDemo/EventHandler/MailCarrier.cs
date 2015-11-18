using System;
using MailingServiceDemo.Event;
using MailingServiceDemo.Query;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.EventHandler
{
    // mail carrier = mail letter = postman = người đưa thư
    public class MailCarrier : IEventSubscriber<MessageQueued>
    {
        private readonly IRequestDispatcher _dispatcher;
        private readonly ISmtpClientWrapper _smtpClient;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger _logger;

        public MailCarrier(IRequestDispatcher dispatcher, ISmtpClientWrapper smtpClient, IEventPublisher eventPublisher, ILogger logger)
        {
            _dispatcher = dispatcher;
            _smtpClient = smtpClient;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public void Handle(MessageQueued @event)
        {
            var urgentMessage = _dispatcher.DispatchQuery(new MostUrgentMessage());

            try
            {
                _smtpClient.Send(urgentMessage.Message);

                _eventPublisher.Publish(new MessageSent
                                        {
                                            MessageId = urgentMessage.Id,
                                            Message = urgentMessage.Message,
                                            SentAt = DateTime.UtcNow
                                        });
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Event details: {urgentMessage.ToJson()}");

                _eventPublisher.Publish(new SendingFailed
                                        {
                                            MessageId = urgentMessage.Id,
                                            Message = urgentMessage.Message,
                                            Reason = exception.Message,
                                            TriedAt = DateTime.UtcNow
                                        });
            }
        }
    }
}
