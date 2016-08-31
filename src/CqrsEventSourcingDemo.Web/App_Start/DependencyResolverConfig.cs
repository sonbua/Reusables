using System;
using CqrsEventSourcingDemo.AggregateRoot;
using CqrsEventSourcingDemo.Command.Tab;
using CqrsEventSourcingDemo.Infrastructure;
using CqrsEventSourcingDemo.ReadModel;
using CqrsEventSourcingDemo.ReadModel.Tab;
using CqrsEventSourcingDemo.Web.Abstractions;
using CqrsEventSourcingDemo.Web.Abstractions.Decorators;
using Reusables.Cqrs;
using Reusables.Diagnostics.Logging;
using Reusables.Diagnostics.Logging.NLog;
using Reusables.EventSourcing;
using Reusables.Validation;
using Reusables.Validation.DataAnnotations;
using Reusables.Web.Mvc5;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace CqrsEventSourcingDemo.Web
{
    public class DependencyResolverConfig
    {
        internal static IServiceProvider ServiceProvider;

        public static void RegisterDependencies()
        {
            var container = new Container {Options = {DefaultScopedLifestyle = new WebRequestLifestyle()}};

            // Container
            container.Register<IServiceProvider>(() => container);

            // Request (command/query) dispatcher
            container.Register<IRequestDispatcher, RequestDispatcher>();

            // Command handlers
            container.Register(typeof (ICommandHandler<>), new[] {typeof (TabService).Assembly});
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (ValidationDecoratorCommandHandler<>));
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (LoggingDecoratorCommandHandler<>));

            // Query handlers
            container.Register(typeof (IQueryHandler<,>), new[] {typeof (TabReadModel).Assembly});

            // Validators
            container.RegisterSingleton(typeof (IValidator<>), typeof (CompositeValidator<>));
            container.AppendToCollection(typeof (IValidator<>), typeof (DataAnnotationsValidator<>));
            container.RegisterCollection(typeof (IValidator<>), typeof (MvcApplication).Assembly);

            // Data annotations validators
            container.Register(typeof (IValidationAttributeValidator<>), new[] {typeof (IValidationAttributeValidator<>).Assembly});

            // Loggers
            container.RegisterSingleton<ILoggerFactory, NLogLoggerFactory>();

            // Action filters
            container.RegisterCollection(typeof (IActionFilter<>), typeof (MvcApplication).Assembly);

            // Repository
            container.Register<IEventStore, InMemoryEventStore>();

            // Aggregate factory
            container.Register<IAggregateFactory, AggregateFactory>();

            // Event publisher
            container.Register<IEventPublisher>(() => new EventPublisher(type => container.GetAllInstances(type), container.GetInstance<ILoggerFactory>()));

            // Event handlers
            container.RegisterCollection(typeof (IEventSubscriber<>), new[] {typeof (TabReadModel).Assembly});

            // View model database
            container.RegisterSingleton<IViewModelDatabase, InMemoryViewModelDatabase>();

            // Verify
            container.Verify();

            System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            System.Web.Mvc.GlobalFilters.Filters.Add(new ActionFilterDispatcher(type => container.GetAllInstances(type)));
            ServiceProvider = container;
        }
    }
}
