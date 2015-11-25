using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
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
    public class Test
    {
        private readonly IServiceProvider _container;

        public Test(ITestOutputHelper testOutputHelper)
        {
            _container = DependencyResolverConfig.StartRegistrations()
                                                 .CoreDependencies()
                                                 .FakeDatabases()
                                                 .FakeSmtpClientWrapper()
                                                 .FakeApplicationSettings()
                                                 .TestOutputHelper(testOutputHelper)
                                                 .EventSubscriberDiagnostics()
                                                 .CommandHandlerDiagnostics()
                                                 .QueryHandlerDiagnostics()
                                                 .SmtpClientDiagnostics()
                                                 .DbContextDiagnostics()
                                                 .VerifyContainer();
        }

        [Fact]
        public void SimulatesBusinessProcess()
        {
            // arrange
            var dispatcher = _container.GetService<IRequestDispatcher>();
            var sendMail1 = new SendMail
                            {
                                Messages = new[]
                                           {
                                               new MailMessage {From = "Delayed 1", Priority = 1},
                                               new MailMessage {From = "Express 1", Priority = 3},
                                               new MailMessage {From = "Standard 1", Priority = 2},
                                           }
                            };
            var sendMail2 = new SendMail
                            {
                                Messages = new[]
                                           {
                                               new MailMessage {From = "Delayed 2", Priority = 1},
                                               new MailMessage {From = "Express 2", Priority = 3},
                                               new MailMessage {From = "Standard 2", Priority = 2},
                                           }
                            };

            // act
            dispatcher.DispatchCommand(sendMail1);
            dispatcher.DispatchCommand(sendMail2);

            // assert
        }
    }

    internal static class ContainerExtensions
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider)
        {
            return (TService) serviceProvider.GetService(typeof (TService));
        }

        public static Container FakeDatabases(this Container container)
        {
            // Database
            container.RegisterSingleton<IDbContext, InMemoryDbContext>();

            return container;
        }

        public static Container FakeSmtpClientWrapper(this Container container)
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

            container.Register(() => wrappers[Randomizer.Next(wrappers.Length - 1)]);

            return container;
        }

        public static Container FakeApplicationSettings(this Container container)
        {
            container.Register<IApplicationSettings, FakeApplicationSettings>();

            return container;
        }

        public static Container TestOutputHelper(this Container container, ITestOutputHelper testOutputHelper)
        {
            container.Register<ILogger, TestOutputLogger>();
            container.Register<ITestOutputHelper>(() => testOutputHelper);
            //container.RegisterDecorator<ITestOutputHelper, NewLineAppender>();

            return container;
        }

        public static Container EventSubscriberDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (IEventSubscriber<>), typeof (EventSubscriberNotifier<>));
            //container.RegisterDecorator(typeof (IEventSubscriber<>), typeof (EventSubscriberDatabaseReporter<>));

            return container;
        }

        public static Container CommandHandlerDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (CommandHandlerNotifier<>));
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (CommandHandlerReporter<>));
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (CommandHandlerStopwatcher<>));

            return container;
        }

        public static Container QueryHandlerDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (IQueryHandler<,>), typeof (QueryHandlerReporter<,>));

            return container;
        }

        public static Container SmtpClientDiagnostics(this Container container)
        {
            container.RegisterDecorator<ISmtpClientWrapper, Firewall>();
            container.RegisterDecorator<ISmtpClientWrapper, SmptClientProcrastinator>();

            return container;
        }

        public static Container DbContextDiagnostics(this Container container)
        {
            container.RegisterDecorator<IDbContext, DbContextSpy>();

            return container;
        }
    }

    public static class Randomizer
    {
        private static readonly Random _random = new Random();

        public static int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }
    }

    public class SuccessSmtpClientWrapper : ISmtpClientWrapper
    {
        public void Send(MailMessage message)
        {
        }

        public async Task SendAsync(MailMessage message)
        {
            await Task.Yield();
        }
    }

    public class FailureSmtpClientWrapper : ISmtpClientWrapper
    {
        private readonly Exception[] _exceptions =
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
            new EntryPointNotFoundException("could not connect to email service"),
        };

        public void Send(MailMessage message)
        {
            throw _exceptions[Randomizer.Next(_exceptions.Length - 1)];
        }

        public Task SendAsync(MailMessage message)
        {
            throw _exceptions[Randomizer.Next(_exceptions.Length - 1)];
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
            _testOutputHelper.WriteLine($">> ERROR: {exception}");
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _testOutputHelper.WriteLine($">> ERROR: {exception.GetType().Name}: {exception.Message}\n{message}");
        }

        public void Error(string message, params object[] args)
        {
            _testOutputHelper.WriteLine(message);
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

    public class NewLineAppender : ITestOutputHelper
    {
        private readonly ITestOutputHelper _innerHelper;

        public NewLineAppender(ITestOutputHelper innerHelper)
        {
            _innerHelper = innerHelper;
        }

        public void WriteLine(string message)
        {
            _innerHelper.WriteLine($"{message}\n");
        }

        public void WriteLine(string format, params object[] args)
        {
            _innerHelper.WriteLine($"{format}\n", args);
        }
    }

    public class EventSubscriberNotifier<TEvent> : IEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IEventSubscriber<TEvent> _innerSubscriber;

        public EventSubscriberNotifier(ILogger logger, IEventSubscriber<TEvent> innerSubscriber)
        {
            _logger = logger;
            _innerSubscriber = innerSubscriber;
        }

        public void Handle(TEvent @event)
        {
            _logger.Info($"{typeof (TEvent).Name}  ====>  {_innerSubscriber.GetType().Name}: {@event.ToJson()}");

            _innerSubscriber.Handle(@event);
        }
    }

    public class EventSubscriberDatabaseReporter<TEvent> : IEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;
        private readonly IEventSubscriber<TEvent> _innerSubscriber;

        public EventSubscriberDatabaseReporter(ILogger logger, IDbContext dbContext, IEventSubscriber<TEvent> innerSubscriber)
        {
            _logger = logger;
            _dbContext = dbContext;
            _innerSubscriber = innerSubscriber;
        }

        public void Handle(TEvent @event)
        {
            _innerSubscriber.Handle(@event);

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
        }
    }

    public class CommandHandlerNotifier<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public CommandHandlerNotifier(ILogger logger, ICommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public void Handle(TCommand command)
        {
            _logger.Info($"{command.GetType().Name}  ====>  {_innerHandler.GetType().Name}: {command.ToJson()}");

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

            _logger.Info($"{typeof (TCommand).Name}: {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public class QueryHandlerReporter<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        private readonly ILogger _logger;
        private readonly IQueryHandler<TQuery, TResult> _innerHandler;

        public QueryHandlerReporter(ILogger logger, IQueryHandler<TQuery, TResult> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public TResult Handle(TQuery query)
        {
            var result = _innerHandler.Handle(query);

            string resultString;

            try
            {
                resultString = result.ToJson();
            }
            catch (Exception)
            {
                resultString = "{}";
            }

            _logger.Info($"{query.GetType().Name}  ====>  {_innerHandler.GetType().Name}: {query.ToJson()} => {resultString}");

            return result;
        }
    }

    public class Firewall : ISmtpClientWrapper
    {
        private readonly ILogger _logger;
        private readonly ISmtpClientWrapper _smtpClient;

        public Firewall(ILogger logger, ISmtpClientWrapper smtpClient)
        {
            _logger = logger;
            _smtpClient = smtpClient;
        }

        public void Send(MailMessage message)
        {
            _logger.Info("Delivering....................");

            _smtpClient.Send(message);
        }

        public async Task SendAsync(MailMessage message)
        {
            _logger.Info("Delivering....................");

            await _smtpClient.SendAsync(message);
        }
    }

    public class SmptClientProcrastinator : ISmtpClientWrapper
    {
        private readonly ILogger _logger;
        private readonly ISmtpClientWrapper _smtpClient;

        public SmptClientProcrastinator(ILogger logger, ISmtpClientWrapper smtpClient)
        {
            _logger = logger;
            _smtpClient = smtpClient;
        }

        public void Send(MailMessage message)
        {
            _smtpClient.Send(message);
        }

        public async Task SendAsync(MailMessage message)
        {
            var delay = Randomizer.Next(100);

            _logger.Info($"Delaying {delay} ms...");

            await Task.Delay(delay);

            await _smtpClient.SendAsync(message);
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
}
