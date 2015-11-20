﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.NetworkInformation;
using MailingServiceDemo.Command;
using MailingServiceDemo.CompositionRoot;
using MailingServiceDemo.Database;
using MailingServiceDemo.Model;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;
using Reusables.Serialization.Newtonsoft.Extensions;
using SimpleInjector;
using Xunit;
using Xunit.Abstractions;

namespace MailingServiceDemo.Tests
{
    public class Tests
    {
        private readonly Container _container;

        public Tests(ITestOutputHelper testOutputHelper)
        {
            _container = DependencyResolverConfig.Build()
                                                 .RegisterDependencies()
                                                 .RegisterRandomizer()
                                                 .RegisterFakeDatabases()
                                                 .RegisterFakeSmtpClientWrapper()
                                                 .RegisterFakeApplicationSettings()
                                                 .RegisterTestOutputHelper(testOutputHelper)
                                                 .RegisterEventSubscriberDiagnostics()
                                                 .RegisterCommandHandlerDiagnostics()
                                                 .RegisterQueryHandlerDiagnostics()
                                                 .RegisterSmtpClientDiagnostics()
                                                 .RegisterDbContextDiagnostics()
                                                 .VerifyContainer();
        }

        [Fact]
        public void SimulatesSynchronousBusinessProcess()
        {
            // arrange
            var dispatcher = _container.GetInstance<IRequestDispatcher>();
            var sendMail1 = new SendMail
                            {
                                Messages = new[]
                                           {
                                               new MailMessage {Subject = "Delayed 1", Priority = MailPriority.Low},
                                               new MailMessage {Subject = "Express 1", Priority = MailPriority.High},
                                               new MailMessage {Subject = "Standard 1", Priority = MailPriority.Normal},
                                           }
                            };
            var sendMail2 = new SendMail
                            {
                                Messages = new[]
                                           {
                                               new MailMessage {Subject = "Delayed 2", Priority = MailPriority.Low},
                                               new MailMessage {Subject = "Express 2", Priority = MailPriority.High},
                                               new MailMessage {Subject = "Standard 2", Priority = MailPriority.Normal},
                                           }
                            };

            // act
            dispatcher.DispatchCommand(sendMail1);
            dispatcher.DispatchCommand(sendMail2);

            // assert
            Assert.True(sendMail1.Id != Guid.Empty);
        }
    }

    internal static class ContainerExtensions
    {
        public static Container RegisterFakeDatabases(this Container container)
        {
            // View model database
            container.RegisterSingleton<IDbContext>(() =>
                                                    {
                                                        var database = new InMemoryDbContext();

                                                        database.Clean();

                                                        return database;
                                                    });

            return container;
        }

        public static Container RegisterFakeSmtpClientWrapper(this Container container)
        {
            var wrappers = new ISmtpClientWrapper[]
                           {
                               new SuccessSmtpClientWrapper(),
                               new FailureSmtpClientWrapper(),
                               new SuccessSmtpClientWrapper(),
                               new FailureSmtpClientWrapper(),
                               new SuccessSmtpClientWrapper(),
                               new FailureSmtpClientWrapper(),
                               new SuccessSmtpClientWrapper(),
                               new FailureSmtpClientWrapper(),
                               new SuccessSmtpClientWrapper(),
                               new FailureSmtpClientWrapper(),
                           };

            container.Register(() => wrappers[container.GetInstance<Random>().Next(wrappers.Length - 1)]);

            return container;
        }

        public static Container RegisterFakeApplicationSettings(this Container container)
        {
            container.Register<IApplicationSettings, FakeApplicationSettings>();

            return container;
        }

        public static Container RegisterTestOutputHelper(this Container container, ITestOutputHelper testOutputHelper)
        {
            container.Register<ILogger, TestOutputLogger>();
            container.RegisterSingleton<ITestOutputHelper>(() => testOutputHelper);

            return container;
        }

        public static Container RegisterEventSubscriberDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (IEventSubscriber<>), typeof (EventSubscriberDiagnostics<>));

            return container;
        }

        public static Container RegisterCommandHandlerDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (CommandHandlerValidator<>));
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (CommandHandlerReporter<>));
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (CommandHandlerStopwatcher<>));

            return container;
        }

        public static Container RegisterQueryHandlerDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (IQueryHandler<,>), typeof (QueryHandlerValidator<,>));

            return container;
        }

        public static Container RegisterRandomizer(this Container container)
        {
            container.RegisterSingleton(() => new Random());

            return container;
        }

        public static Container RegisterSmtpClientDiagnostics(this Container container)
        {
            container.RegisterDecorator<ISmtpClientWrapper, NetworkSpy>();

            return container;
        }

        public static Container RegisterDbContextDiagnostics(this Container container)
        {
            container.RegisterDecorator<IDbContext, DbContextSpy>();

            return container;
        }
    }

    public class DbContextSpy : IDbContext
    {
        private readonly IDbContext _victimDbContext;
        private readonly IServiceProvider _serviceProvider;

        public DbContextSpy(IDbContext victimDbContext, IServiceProvider serviceProvider)
        {
            _victimDbContext = victimDbContext;
            _serviceProvider = serviceProvider;
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            var logger = (ILogger) _serviceProvider.GetService(typeof (ILogger));

            return new DbSetSpy<TEntity>(logger, _victimDbContext.Set<TEntity>());
        }
    }

    public class DbSetSpy<TEntity> : IDbSet<TEntity> where TEntity : Entity
    {
        private readonly ILogger _logger;
        private readonly IDbSet<TEntity> _victimDbSet;

        public DbSetSpy(ILogger logger, IDbSet<TEntity> victimDbSet)
        {
            _logger = logger;
            _victimDbSet = victimDbSet;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _victimDbSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TEntity entity)
        {
            _logger.Info($"{nameof(Add)} {entity.GetType().Name}: {entity.ToJson()}");

            _victimDbSet.Add(entity);
        }

        public void Update(Guid id, Action<TEntity> updateAction)
        {
            _logger.Info($"{nameof(Update)} {typeof (TEntity).Name}: {updateAction.ToJson()}");

            _victimDbSet.Update(id, updateAction);
        }

        public void Remove(Guid id)
        {
            _logger.Info($"{nameof(Remove)} {typeof (TEntity).Name}: {id}");

            _victimDbSet.Remove(id);
        }

        public TEntity GetById(Guid id)
        {
            _logger.Info($"{nameof(GetById)} {typeof (TEntity).Name}: {id}");

            var entity = _victimDbSet.GetById(id);

            _logger.Info($"  > {entity.ToJson()}");

            return entity;
        }
    }

    public class NetworkSpy : ISmtpClientWrapper
    {
        private readonly ILogger _logger;
        private readonly ISmtpClientWrapper _smtpClientWrapper;

        public NetworkSpy(ILogger logger, ISmtpClientWrapper smtpClientWrapper)
        {
            _logger = logger;
            _smtpClientWrapper = smtpClientWrapper;
        }

        public void Send(MailMessage message)
        {
            _logger.Info("Delivering....................\n");

            _smtpClientWrapper.Send(message);
        }
    }

    public class SuccessSmtpClientWrapper : ISmtpClientWrapper
    {
        public void Send(MailMessage message)
        {
        }
    }

    public class FailureSmtpClientWrapper : ISmtpClientWrapper
    {
        private static readonly Random _randomizer = new Random();

        public void Send(MailMessage message)
        {
            var exceptions = new Exception[]
                             {
                                 new NetworkInformationException(1),
                                 new NetworkInformationException(2),
                                 new NetworkInformationException(3),
                                 new NetworkInformationException(4),
                                 new NetworkInformationException(5),
                                 new NetworkInformationException(6),
                                 new NetworkInformationException(7),
                                 new NetworkInformationException(8),
                                 new NetworkInformationException(9),
                                 new SmtpException("unauthorized access"),
                                 new EntryPointNotFoundException("could not connect to email service"),
                             };

            throw exceptions[_randomizer.Next(exceptions.Length - 1)];
        }
    }

    public class FakeApplicationSettings : IApplicationSettings
    {
        public int MaxAttempt => 3;
    }

    public class TestOutputLogger : ILogger
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TestOutputLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public void Debug(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Debug(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] args)
        {
            _testOutputHelper.WriteLine($"{message}");
        }

        public void Warn(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception)
        {
            _testOutputHelper.WriteLine($">> ERROR: {exception}\n");
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _testOutputHelper.WriteLine($">> ERROR: {exception.GetType().Name}: {exception.Message}\n{message}\n");
        }

        public void Error(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, params object[] args)
        {
            throw new NotImplementedException();
        }
    }

    public class EventSubscriberDiagnostics<TEvent> : IEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IEventSubscriber<TEvent> _innerEventSubscriber;

        public EventSubscriberDiagnostics(ILogger logger, IEventSubscriber<TEvent> innerEventSubscriber)
        {
            _logger = logger;
            _innerEventSubscriber = innerEventSubscriber;
        }

        public void Handle(TEvent @event)
        {
            _logger.Info($"{typeof (TEvent).Name}  ====>  {_innerEventSubscriber.GetType().Name}: {@event.ToJson()}\n");

            _innerEventSubscriber.Handle(@event);
        }
    }

    public class CommandHandlerValidator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public CommandHandlerValidator(ILogger logger, ICommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public void Handle(TCommand command)
        {
            _logger.Info($"{command.GetType().Name}  ====>  {_innerHandler.GetType().Name}: {command.ToJson()}\n");

            _innerHandler.Handle(command);
        }
    }

    public class CommandHandlerReporter<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _innerHandler;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public CommandHandlerReporter(ICommandHandler<TCommand> innerHandler, IDbContext dbContext, ILogger logger)
        {
            _innerHandler = innerHandler;
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Handle(TCommand command)
        {
            _innerHandler.Handle(command);

            _logger.Info($"Handled {typeof (TCommand).Name} command: {command.ToJson()}");

            _logger.Info($">> {nameof(OutboxMessage)} table:");
            foreach (var message in _dbContext.Set<OutboxMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }

            _logger.Info($">> {nameof(OngoingMessage)} table:");
            foreach (var message in _dbContext.Set<OngoingMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }

            _logger.Info($">> {nameof(FaultMessage)} table:");
            foreach (var message in _dbContext.Set<FaultMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }

            _logger.Info($">> {nameof(SentMessage)} table:");
            foreach (var message in _dbContext.Set<SentMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }

            _logger.Info($">> {nameof(SuspiciousMessage)} table:");
            foreach (var message in _dbContext.Set<SuspiciousMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }

            _logger.Info(string.Empty);
        }
    }

    public class CommandHandlerStopwatcher<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public CommandHandlerStopwatcher(ILogger logger, ICommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public void Handle(TCommand command)
        {
            var stopwatch = Stopwatch.StartNew();

            stopwatch.Start();

            _innerHandler.Handle(command);

            stopwatch.Stop();

            _logger.Info($"{typeof (TCommand).Name}: {stopwatch.ElapsedMilliseconds} ms\n");
        }
    }

    public class QueryHandlerValidator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        private readonly ILogger _logger;
        private readonly IQueryHandler<TQuery, TResult> _innerHandler;

        public QueryHandlerValidator(ILogger logger, IQueryHandler<TQuery, TResult> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public TResult Handle(TQuery query)
        {
            _logger.Info($"{query.GetType().Name}  ====>  {_innerHandler.GetType().Name}: {query.ToJson()}\n");

            return _innerHandler.Handle(query);
        }
    }
}
