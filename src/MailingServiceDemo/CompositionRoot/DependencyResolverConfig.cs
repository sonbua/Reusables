using System;
using MailingServiceDemo.Database;
using NLog;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging.NLog;
using Reusables.EventSourcing;
using Reusables.Validation;
using Reusables.Validation.DataAnnotations;
using SimpleInjector;
using SimpleInjector.Advanced;
using ILogger = Reusables.Diagnostics.Logging.ILogger;

namespace MailingServiceDemo.CompositionRoot
{
    public static class DependencyResolverConfig
    {
        public static Container Build()
        {
            return new Container();
        }

        public static Container RegisterDependencies(this Container container)
        {
            // Container
            container.Register<IServiceProvider>(() => container);

            // Request (command/query) dispatcher
            container.Register<IRequestDispatcher, RequestDispatcher>();

            // Command handlers
            container.Register(typeof (ICommandHandler<>), new[] {typeof (DependencyResolverConfig).Assembly});

            // Query handlers
            container.Register(typeof (IQueryHandler<,>), new[] {typeof (DependencyResolverConfig).Assembly});

            // Validators
            container.RegisterSingleton(typeof (IValidator<>), typeof (CompositeValidator<>));
            container.AppendToCollection(typeof (IValidator<>), typeof (DataAnnotationsValidator<>));
            container.RegisterCollection(typeof (IValidator<>), typeof (DependencyResolverConfig).Assembly);

            // Data annotations validators
            container.Register(typeof (IValidationAttributeValidator<>), new[] {typeof (IValidationAttributeValidator<>).Assembly});

            // Event publisher
            container.Register<IEventPublisher>(() => new EventPublisher(container.GetAllInstances, container.GetInstance<ILogger>()));

            // Event subscribers
            container.RegisterCollection(typeof (IEventSubscriber<>), new[] {typeof (DependencyResolverConfig).Assembly});

            return container;
        }

        public static Container RegisterLoggers(this Container container)
        {
            // Loggers
            container.RegisterSingleton<ILogger, NLogLogger>();
            container.RegisterSingleton(() => LogManager.GetLogger("NLog"));

            return container;
        }

        public static Container RegisterDatabases(this Container container)
        {
            // Database
            container.RegisterSingleton<IDbContext, InMemoryDbContext>();

            return container;
        }

        public static Container VerifyContainer(this Container container)
        {
            // Verify
            container.Verify();

            return container;
        }
    }
}
