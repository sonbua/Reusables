using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
    public class AsyncTest
    {
        private readonly IServiceProvider _container;

        public AsyncTest(ITestOutputHelper testOutputHelper)
        {
            _container = DependencyResolverConfig.StartRegistrations()
                                                 .CoreDependencies()
                                                 .FakeDatabases()
                                                 .FakeSmtpClientWrapper()
                                                 .FakeApplicationSettings()
                                                 .TestOutputHelper(testOutputHelper)
                                                 .ThreadIdAndTimeLogger()
                                                 .AsyncEventSubscriberDiagnostics()
                                                 .AsyncCommandHandlerDiagnostics()
                                                 .AsyncQueryHandlerDiagnostics()
                                                 .SmtpClientDiagnostics()
                                                 .DbContextDiagnostics()
                                                 .VerifyContainer();
        }

        [Fact]
        public async Task SimulatesAsynchronousBusinessProcess()
        {
            // arrange
            var dispatcher = _container.GetService<IRequestDispatcher>();
            var sendMail1 = new SendMail {Messages = Enumerable.Range(1, Randomizer.Next(30)).Select(x => new MailMessage {Priority = Randomizer.Next(100)}).ToArray()};
            var sendMail2 = new SendMail {Messages = Enumerable.Range(1, Randomizer.Next(30)).Select(x => new MailMessage {Priority = Randomizer.Next(100)}).ToArray()};
            var sendMail3 = new SendMail {Messages = Enumerable.Range(1, Randomizer.Next(30)).Select(x => new MailMessage {Priority = Randomizer.Next(100)}).ToArray()};

            // act
            await Task.WhenAll(dispatcher.DispatchCommandAsync(sendMail1),
                               dispatcher.DispatchCommandAsync(sendMail2),
                               dispatcher.DispatchCommandAsync(sendMail3));

            // assert
        }
    }

    internal static class ContainerAsyncExtensions
    {
        public static Container ThreadIdAndTimeLogger(this Container container)
        {
            container.RegisterDecorator<ITestOutputHelper, ThreadIdPrepender>();
            container.RegisterDecorator<ITestOutputHelper, TimeKeeper>();

            return container;
        }

        public static Container AsyncEventSubscriberDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (IAsyncEventSubscriber<>), typeof (AsyncEventSubscriberNotifier<>));
            container.RegisterDecorator(typeof (IAsyncEventSubscriber<>), typeof (AsyncEventSubscriberExceptionSuppressor<>));
            //container.RegisterDecorator(typeof (IAsyncEventSubscriber<>), typeof (EventProcrastinator<>));

            return container;
        }

        public static Container AsyncCommandHandlerDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (IAsyncCommandHandler<>), typeof (AsyncCommandHandlerNotifier<>));
            container.RegisterDecorator(typeof (IAsyncCommandHandler<>), typeof (AsyncCommandHandlerReporter<>));
            container.RegisterDecorator(typeof (IAsyncCommandHandler<>), typeof (AsyncCommandHandlerStopwatcher<>));

            return container;
        }

        public static Container AsyncQueryHandlerDiagnostics(this Container container)
        {
            container.RegisterDecorator(typeof (IAsyncQueryHandler<,>), typeof (AsyncQueryHandlerReporter<,>));

            return container;
        }
    }

    public class ThreadIdPrepender : ITestOutputHelper
    {
        private readonly ITestOutputHelper _innerHelper;

        public ThreadIdPrepender(ITestOutputHelper innerHelper)
        {
            _innerHelper = innerHelper;
        }

        public void WriteLine(string message)
        {
            _innerHelper.WriteLine($"thread #{Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(2)} | {message}");
        }

        public void WriteLine(string format, params object[] args)
        {
            _innerHelper.WriteLine($"thread #{Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(2)} | {format}", args);
        }
    }

    public class TimeKeeper : ITestOutputHelper
    {
        private readonly ITestOutputHelper _innerHelper;

        public TimeKeeper(ITestOutputHelper innerHelper)
        {
            _innerHelper = innerHelper;
        }

        public void WriteLine(string message)
        {
            _innerHelper.WriteLine($"{DateTime.UtcNow.ToString("mm:ss.fff")} | {message}");
        }

        public void WriteLine(string format, params object[] args)
        {
            _innerHelper.WriteLine($"{DateTime.UtcNow.ToString("mm:ss.fff")} | {format}", args);
        }
    }

    public class AsyncEventSubscriberExceptionSuppressor<TEvent> : IAsyncEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IAsyncEventSubscriber<TEvent> _innerSubscriber;

        public AsyncEventSubscriberExceptionSuppressor(ILogger logger, IAsyncEventSubscriber<TEvent> innerSubscriber)
        {
            _logger = logger;
            _innerSubscriber = innerSubscriber;
        }

        public async Task HandleAsync(TEvent @event)
        {
            try
            {
                await _innerSubscriber.HandleAsync(@event);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
        }
    }

    public class AsyncEventSubscriberNotifier<TEvent> : IAsyncEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IAsyncEventSubscriber<TEvent> _innerEventSubscriber;

        public AsyncEventSubscriberNotifier(ILogger logger, IAsyncEventSubscriber<TEvent> innerEventSubscriber)
        {
            _logger = logger;
            _innerEventSubscriber = innerEventSubscriber;
        }

        public async Task HandleAsync(TEvent @event)
        {
            _logger.Info($"{typeof (TEvent).Name}  ====>  {_innerEventSubscriber.GetType().Name}: {@event.ToJson()}");

            await _innerEventSubscriber.HandleAsync(@event);
        }
    }

    public class EventProcrastinator<TEvent> : IAsyncEventSubscriber<TEvent>
    {
        private readonly ILogger _logger;
        private readonly IAsyncEventSubscriber<TEvent> _innerEventSubscriber;

        public EventProcrastinator(ILogger logger, IAsyncEventSubscriber<TEvent> innerEventSubscriber)
        {
            _logger = logger;
            _innerEventSubscriber = innerEventSubscriber;
        }

        public async Task HandleAsync(TEvent @event)
        {
            var delay = Randomizer.Next(100);

            _logger.Info($"Delaying {delay} ms...");

            await Task.Delay(delay);

            await _innerEventSubscriber.HandleAsync(@event);
        }
    }

    public class AsyncCommandHandlerNotifier<TCommand> : IAsyncCommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly IAsyncCommandHandler<TCommand> _innerHandler;

        public AsyncCommandHandlerNotifier(ILogger logger, IAsyncCommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task HandleAsync(TCommand command)
        {
            _logger.Info($"{command.GetType().Name}  ====>  {_innerHandler.GetType().Name}: {command.ToJson()}");

            await _innerHandler.HandleAsync(command);
        }
    }

    public class AsyncCommandHandlerReporter<TCommand> : IAsyncCommandHandler<TCommand>
    {
        private readonly IAsyncCommandHandler<TCommand> _innerHandler;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public AsyncCommandHandlerReporter(IAsyncCommandHandler<TCommand> innerHandler, IDbContext dbContext, ILogger logger)
        {
            _innerHandler = innerHandler;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task HandleAsync(TCommand command)
        {
            await _innerHandler.HandleAsync(command);

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

    public class AsyncCommandHandlerStopwatcher<TCommand> : IAsyncCommandHandler<TCommand>
    {
        private readonly ILogger _logger;
        private readonly IAsyncCommandHandler<TCommand> _innerHandler;

        public AsyncCommandHandlerStopwatcher(ILogger logger, IAsyncCommandHandler<TCommand> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task HandleAsync(TCommand command)
        {
            var stopwatch = Stopwatch.StartNew();

            stopwatch.Start();

            await _innerHandler.HandleAsync(command);

            stopwatch.Stop();

            _logger.Info($"{typeof (TCommand).Name}: {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public class AsyncQueryHandlerReporter<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult> where TQuery : Query<TResult>
    {
        private readonly ILogger _logger;
        private readonly IAsyncQueryHandler<TQuery, TResult> _innerHandler;

        public AsyncQueryHandlerReporter(ILogger logger, IAsyncQueryHandler<TQuery, TResult> innerHandler)
        {
            _logger = logger;
            _innerHandler = innerHandler;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            var result = await _innerHandler.HandleAsync(query).ConfigureAwait(false);

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
}
