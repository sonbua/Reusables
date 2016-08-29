using System;
using System.Linq;
using System.Threading.Tasks;
using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using MailingServiceDemo.Model;
using Reusables.EventSourcing;

namespace MailingServiceDemo.EventHandler
{
    public class StoreKeeper : IEventSubscriber<MailRequestAccepted>,
                               IEventSubscriber<MessageSent>,
                               IEventSubscriber<SendingFailed>,
                               IEventSubscriber<FaultAnalysisRequired>,
                               IEventSubscriber<FaultMessageRequeueNeeded>,
                               IEventSubscriber<ManualAnalysisRequired>,
                               IAsyncEventSubscriber<MailRequestAccepted>,
                               IAsyncEventSubscriber<MessageSent>,
                               IAsyncEventSubscriber<SendingFailed>,
                               IAsyncEventSubscriber<FaultAnalysisRequired>,
                               IAsyncEventSubscriber<FaultMessageRequeueNeeded>,
                               IAsyncEventSubscriber<ManualAnalysisRequired>
    {
        private readonly IDbContext _dbContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly IApplicationSettings _settings;

        public StoreKeeper(IDbContext dbContext, IEventPublisher eventPublisher, IApplicationSettings settings)
        {
            _dbContext = dbContext;
            _eventPublisher = eventPublisher;
            _settings = settings;
        }

        public void Handle(MailRequestAccepted @event)
        {
            foreach (var mailMessage in @event.Messages)
            {
                _dbContext.Set<OutboxMessage>().Add(new OutboxMessage
                                                    {
                                                        Id = Guid.NewGuid(),
                                                        RequestId = @event.Id,
                                                        Message = mailMessage,
                                                        Priority = mailMessage.Priority,
                                                        QueuedAt = DateTime.UtcNow
                                                    });
            }

            _eventPublisher.Publish(new OutboxManagementNeeded());
        }

        public void Handle(MessageSent @event)
        {
            _dbContext.Set<SentMessage>().Add(new SentMessage
                                              {
                                                  Id = @event.MessageId,
                                                  RequestId = @event.RequestId,
                                                  Message = @event.Message,
                                                  SentAt = @event.SentAt
                                              });

            _dbContext.Set<OngoingMessage>().Remove(@event.MessageId);

            _eventPublisher.Publish(new OutboxManagementNeeded());
        }

        public void Handle(SendingFailed @event)
        {
            _dbContext.Set<FaultMessage>().Add(new FaultMessage
                                               {
                                                   Id = Guid.NewGuid(),
                                                   RequestId = @event.RequestId,
                                                   MessageId = @event.MessageId,
                                                   Message = @event.Message,
                                                   Reason = @event.Reason,
                                                   TriedAt = @event.TriedAt
                                               });

            _eventPublisher.Publish(new FaultAnalysisRequired
                                    {
                                        MessageId = @event.MessageId,
                                        Message = @event.Message
                                    });

            _eventPublisher.Publish(new OutboxManagementNeeded());
        }

        public void Handle(FaultAnalysisRequired @event)
        {
            var attemptCount = _dbContext.Set<FaultMessage>().Count(message => message.MessageId == @event.MessageId);

            if (attemptCount >= _settings.MaxAttempt)
            {
                _eventPublisher.Publish(new ManualAnalysisRequired
                                        {
                                            MessageId = @event.MessageId,
                                            Message = @event.Message
                                        });

                return;
            }

            _eventPublisher.Publish(new FaultMessageRequeueNeeded
                                    {
                                        MessageId = @event.MessageId
                                    });
        }

        public void Handle(FaultMessageRequeueNeeded @event)
        {
            var ongoingMessage = _dbContext.Set<OngoingMessage>().GetById(@event.MessageId);

            _dbContext.Set<OutboxMessage>().Add(new OutboxMessage
                                                {
                                                    Id = @event.MessageId,
                                                    RequestId = ongoingMessage.RequestId,
                                                    Message = ongoingMessage.Message,
                                                    Priority = ongoingMessage.Priority,
                                                    QueuedAt = DateTime.UtcNow
                                                });

            _dbContext.Set<OngoingMessage>().Remove(@event.MessageId);
        }

        public void Handle(ManualAnalysisRequired @event)
        {
            _dbContext.Set<SuspiciousMessage>().Add(new SuspiciousMessage
                                                    {
                                                        Id = Guid.NewGuid(),
                                                        MessageId = @event.MessageId,
                                                        Message = @event.Message
                                                    });

            _dbContext.Set<OngoingMessage>().Remove(@event.MessageId);
        }

        public async Task HandleAsync(MailRequestAccepted @event)
        {
            foreach (var mailMessage in @event.Messages)
            {
                _dbContext.Set<OutboxMessage>().Add(new OutboxMessage
                                                    {
                                                        Id = Guid.NewGuid(),
                                                        RequestId = @event.Id,
                                                        Message = mailMessage,
                                                        Priority = mailMessage.Priority,
                                                        QueuedAt = DateTime.UtcNow
                                                    });
            }

            await _eventPublisher.PublishAsync(new OutboxManagementNeeded()).ConfigureAwait(false);
        }

        public async Task HandleAsync(MessageSent @event)
        {
            _dbContext.Set<SentMessage>().Add(new SentMessage
                                              {
                                                  Id = @event.MessageId,
                                                  RequestId = @event.RequestId,
                                                  Message = @event.Message,
                                                  SentAt = @event.SentAt
                                              });

            _dbContext.Set<OngoingMessage>().Remove(@event.MessageId);

            await _eventPublisher.PublishAsync(new OutboxManagementNeeded()).ConfigureAwait(false);
        }

        public async Task HandleAsync(SendingFailed @event)
        {
            _dbContext.Set<FaultMessage>().Add(new FaultMessage
                                               {
                                                   Id = Guid.NewGuid(),
                                                   RequestId = @event.RequestId,
                                                   MessageId = @event.MessageId,
                                                   Message = @event.Message,
                                                   Reason = @event.Reason,
                                                   TriedAt = @event.TriedAt
                                               });

            await _eventPublisher.PublishAsync(new FaultAnalysisRequired
                                               {
                                                   MessageId = @event.MessageId,
                                                   Message = @event.Message
                                               }).ConfigureAwait(false);

            await _eventPublisher.PublishAsync(new OutboxManagementNeeded()).ConfigureAwait(false);
        }

        public async Task HandleAsync(FaultAnalysisRequired @event)
        {
            var attemptCount = _dbContext.Set<FaultMessage>().Count(message => message.MessageId == @event.MessageId);

            if (attemptCount >= _settings.MaxAttempt)
            {
                await _eventPublisher.PublishAsync(new ManualAnalysisRequired
                                                   {
                                                       MessageId = @event.MessageId,
                                                       Message = @event.Message
                                                   }).ConfigureAwait(false);

                return;
            }

            await _eventPublisher.PublishAsync(new FaultMessageRequeueNeeded {MessageId = @event.MessageId}).ConfigureAwait(false);
        }

        public async Task HandleAsync(FaultMessageRequeueNeeded @event)
        {
            var ongoingMessage = _dbContext.Set<OngoingMessage>().GetById(@event.MessageId);

            _dbContext.Set<OutboxMessage>().Add(new OutboxMessage
                                                {
                                                    Id = @event.MessageId,
                                                    RequestId = ongoingMessage.RequestId,
                                                    Message = ongoingMessage.Message,
                                                    Priority = ongoingMessage.Priority,
                                                    QueuedAt = DateTime.UtcNow
                                                });

            _dbContext.Set<OngoingMessage>().Remove(@event.MessageId);

            await Task.Yield();
        }

        public async Task HandleAsync(ManualAnalysisRequired @event)
        {
            _dbContext.Set<SuspiciousMessage>().Add(new SuspiciousMessage
                                                    {
                                                        Id = Guid.NewGuid(),
                                                        MessageId = @event.MessageId,
                                                        Message = @event.Message
                                                    });

            _dbContext.Set<OngoingMessage>().Remove(@event.MessageId);

            await Task.Yield();
        }
    }
}
