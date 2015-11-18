using System;
using System.Net.Mail;
using System.Net.NetworkInformation;
using MailingServiceDemo.Command;
using MailingServiceDemo.CompositionRoot;
using MailingServiceDemo.Database;
using MailingServiceDemo.ReadModel;
using Ploeh.AutoFixture.Xunit2;
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
                                                 .RegisterEventSubscriberDecorators(testOutputHelper)
                                                 .RegisterCommandHandlerDiagnostics()
                                                 .RegisterSmtpClientDecorator()
                                                 .VerifyContainer();
        }

        [Theory]
        [AutoData]
        public void SimulatesBusinessProcess()
        {
            // arrange
            var dispatcher = _container.GetInstance<IRequestDispatcher>();
            var sendBatchMail1 = new SendBatchMail
                                 {
                                     Messages = new[]
                                                {
                                                    new MailMessage {Subject = "Delay 1", Priority = MailPriority.Low},
                                                    new MailMessage {Subject = "Express 1", Priority = MailPriority.High},
                                                    new MailMessage {Subject = "Standard 1", Priority = MailPriority.Normal},
                                                }
                                 };
            var sendBatchMail2 = new SendBatchMail
                                 {
                                     Messages = new[]
                                                {
                                                    new MailMessage {Subject = "Delay 2", Priority = MailPriority.Low},
                                                    new MailMessage {Subject = "Express 2", Priority = MailPriority.High},
                                                    new MailMessage {Subject = "Standard 2", Priority = MailPriority.Normal},
                                                }
                                 };

            // act
            dispatcher.DispatchCommand(sendBatchMail1);
            dispatcher.DispatchCommand(sendBatchMail2);

            // assert
            Assert.True(sendBatchMail1.Id != Guid.Empty);
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

        public static Container RegisterEventSubscriberDecorators(this Container container, ITestOutputHelper testOutputHelper)
        {
            container.Register<ILogger, TestOutputLogger>();
            container.RegisterSingleton<ITestOutputHelper>(() => testOutputHelper);

            container.RegisterDecorator(typeof (IEventSubscriber<>), typeof (EventSubscriberDiagnostics<>));

            return container;
        }

        public static Container RegisterCommandHandlerDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (CommandHandlerDiagnostics<>));

            return container;
        }

        public static Container RegisterRandomizer(this Container container)
        {
            container.RegisterSingleton(() => new Random());

            return container;
        }

        public static Container RegisterSmtpClientDecorator(this Container container)
        {
            container.RegisterDecorator<ISmtpClientWrapper, Spy>();

            return container;
        }
    }

    public class Spy : ISmtpClientWrapper
    {
        private readonly ILogger _logger;
        private readonly ISmtpClientWrapper _smtpClientWrapper;

        public Spy(ILogger logger, ISmtpClientWrapper smtpClientWrapper)
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

    public class CommandHandlerDiagnostics<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _innerHandler;
        private readonly IDbContext _database;
        private readonly ILogger _logger;

        public CommandHandlerDiagnostics(ICommandHandler<TCommand> innerHandler, IDbContext database, ILogger logger)
        {
            _innerHandler = innerHandler;
            _database = database;
            _logger = logger;
        }

        public void Handle(TCommand command)
        {
            _innerHandler.Handle(command);

            _logger.Info($"Handled {typeof (TCommand).Name} command: {command.ToJson()}");

            _logger.Info($">> {nameof(OutboxMessage)} table:");
            foreach (var message in _database.Set<OutboxMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }

            _logger.Info($">> {nameof(FaultMessage)} table:");
            foreach (var message in _database.Set<FaultMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }

            _logger.Info($">> {nameof(SentMessage)} table:");
            foreach (var message in _database.Set<SentMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }

            _logger.Info($">> {nameof(SuspiciousMessage)} table:");
            foreach (var message in _database.Set<SuspiciousMessage>())
            {
                _logger.Info($"   > {message.ToJson()}");
            }
        }
    }
}
