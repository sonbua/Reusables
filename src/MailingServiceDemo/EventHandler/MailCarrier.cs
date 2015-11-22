using System;
using System.Threading.Tasks;
using MailingServiceDemo.Event;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace MailingServiceDemo.EventHandler
{
    // mail carrier = mail letter = postman = người đưa thư
    public class MailCarrier : IEventSubscriber<DeliveryReady>,
                               IAsyncEventSubscriber<DeliveryReady>
    {
        private readonly ISmtpClientWrapper _smtpClient;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger _logger;

        public MailCarrier(ISmtpClientWrapper smtpClient, IEventPublisher eventPublisher, ILogger logger)
        {
            _smtpClient = smtpClient;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public void Handle(DeliveryReady @event)
        {
            var delivery = @event.Message;

            try
            {
                _smtpClient.Send(delivery.Message);

                _eventPublisher.Publish(new MessageSent
                                        {
                                            RequestId = delivery.RequestId,
                                            MessageId = delivery.Id,
                                            Message = delivery.Message,
                                            SentAt = DateTime.UtcNow
                                        });
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Event details: {delivery.ToJson()}");

                _eventPublisher.Publish(new SendingFailed
                                        {
                                            RequestId = delivery.RequestId,
                                            MessageId = delivery.Id,
                                            Message = delivery.Message,
                                            Reason = exception.Message,
                                            TriedAt = DateTime.UtcNow
                                        });
            }
        }

        public async Task HandleAsync(DeliveryReady @event)
        {
            var delivery = @event.Message;

            try
            {
                await _smtpClient.SendAsync(delivery.Message).ConfigureAwait(false);

                await _eventPublisher.PublishAsync(new MessageSent
                                                   {
                                                       RequestId = delivery.RequestId,
                                                       MessageId = delivery.Id,
                                                       Message = delivery.Message,
                                                       SentAt = DateTime.UtcNow
                                                   }).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Event details: {delivery.ToJson()}");

                await _eventPublisher.PublishAsync(new SendingFailed
                                                   {
                                                       RequestId = delivery.RequestId,
                                                       MessageId = delivery.Id,
                                                       Message = delivery.Message,
                                                       Reason = exception.Message,
                                                       TriedAt = DateTime.UtcNow
                                                   }).ConfigureAwait(false);
            }
        }
    }
}
