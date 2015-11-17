﻿using System.Linq;
using MailingServiceDemo.Database;
using MailingServiceDemo.Event;
using Reusables.EventSourcing;

namespace MailingServiceDemo.ReadModel
{
    public class FailureMessageReadModel : IEventSubscriber<SendingFailed>
    {
        private readonly IViewModelDatabase _database;
        private readonly IApplicationSettings _settings;
        private readonly IEventPublisher _eventPublisher;

        public FailureMessageReadModel(IViewModelDatabase database, IApplicationSettings settings, IEventPublisher eventPublisher)
        {
            _database = database;
            _settings = settings;
            _eventPublisher = eventPublisher;
        }

        public void Handle(SendingFailed @event)
        {
            _database.Set<FailureMessage>().Add(new FailureMessage
                                                {
                                                    MessageId = @event.MessageId,
                                                    Message = @event.Message,
                                                    Reason = @event.Reason,
                                                    TriedAt = @event.TriedAt
                                                });

            var attemptCount = _database.Set<FailureMessage>().Count(message => message.MessageId == @event.MessageId);
            if (attemptCount >= _settings.MaxAttempt)
            {
                return;
            }

            _eventPublisher.Publish(new MessageQueued
                                    {
                                        MessageId = @event.MessageId,
                                        Message = @event.Message
                                    });
        }
    }
}
