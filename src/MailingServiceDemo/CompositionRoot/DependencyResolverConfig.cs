using System;
using MailingServiceDemo.Database;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Diagnostics.Logging.NLog;
using Reusables.EventSourcing;
using Reusables.Validation;
using Reusables.Validation.DataAnnotations;
using SimpleInjector;
using SimpleInjector.Advanced;

namespace MailingServiceDemo.CompositionRoot
{
    public static class DependencyResolverConfig
    {
        public static Container StartRegistrations()
        {
            return new Container();
        }

        public static Container CoreDependencies(this Container container)
        {
            // Container
            container.Register<IServiceProvider>(() => container);

            // Request (command/query) dispatcher
            container.Register<IRequestDispatcher, RequestDispatcher>();

            // Command handlers
            container.Register(typeof (ICommandHandler<>), new[] {typeof (DependencyResolverConfig).Assembly});
            container.Register(typeof (IAsyncCommandHandler<>), new[] {typeof (DependencyResolverConfig).Assembly});

            // Query handlers
            container.Register(typeof (IQueryHandler<,>), new[] {typeof (DependencyResolverConfig).Assembly});
            container.Register(typeof (IAsyncQueryHandler<,>), new[] {typeof (DependencyResolverConfig).Assembly});

            // Validators
            container.Register(typeof (IValidator<>), typeof (CompositeValidator<>));
            container.AppendToCollection(typeof (IValidator<>), typeof (DataAnnotationsValidator<>));
            container.RegisterCollection(typeof (IValidator<>), typeof (DependencyResolverConfig).Assembly);

            // Data annotations validators
            container.Register(typeof (IValidationAttributeValidator<>), new[] {typeof (IValidationAttributeValidator<>).Assembly});

            // Event publisher
            container.Register<IEventPublisher>(() => new EventPublisher(container.GetAllInstances, container.GetInstance<ILoggerFactory>()));

            // Event subscribers
            container.RegisterCollection(typeof (IEventSubscriber<>), new[] {typeof (DependencyResolverConfig).Assembly});
            container.RegisterCollection(typeof (IAsyncEventSubscriber<>), new[] {typeof (DependencyResolverConfig).Assembly});

            return container;
        }

        public static Container Loggers(this Container container)
        {
            // Loggers
            container.RegisterSingleton<ILoggerFactory, NLogLoggerFactory>();

            return container;
        }

        public static Container Databases(this Container container)
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
