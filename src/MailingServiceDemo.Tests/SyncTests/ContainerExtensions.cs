using System;
using MailingServiceDemo.Database;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.EventSourcing;
using SimpleInjector;
using Xunit.Abstractions;

namespace MailingServiceDemo.Tests.SyncTests
{
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
            container.Register<ILoggerFactory, TestOutputLoggerFactory>();
            container.Register<ILogger, TestOutputLogger>();
            container.Register<ITestOutputHelper>(() => testOutputHelper);
            container.RegisterDecorator<ITestOutputHelper, NewLineAppender>();

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
}