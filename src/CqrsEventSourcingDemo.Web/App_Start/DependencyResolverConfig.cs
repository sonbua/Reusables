using System;
using System.Collections.Generic;
using CqrsEventSourcingDemo.Web.Abstractions;
using CqrsEventSourcingDemo.Web.Abstractions.Decorators;
using NLog;
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
using ILogger = Reusables.Diagnostics.Logging.ILogger;

namespace CqrsEventSourcingDemo.Web
{
    public class DependencyResolverConfig
    {
        public static void RegisterDependencies()
        {
            var container = new Container {Options = {DefaultScopedLifestyle = new WebRequestLifestyle()}};

            // Container
            container.Register<IServiceProvider>(() => container);

            // Request (command/query) dispatcher
            container.Register<IRequestDispatcher, RequestDispatcher>();

            // Command handlers
            container.Register(typeof (ICommandHandler<>), new[] {typeof (MvcApplication).Assembly});
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (ValidationDecoratorCommandHandler<>));
            container.RegisterDecorator(typeof (ICommandHandler<>), typeof (LoggingDecoratorCommandHandler<>));

            // Query handlers
            container.Register(typeof (IQueryHandler<,>), new[] {typeof (MvcApplication).Assembly});

            // Validators
            container.RegisterSingleton(typeof (IValidator<>), typeof (CompositeValidator<>));
            container.AppendToCollection(typeof (IValidator<>), typeof (DataAnnotationsValidator<>));
            container.RegisterCollection(typeof (IValidator<>), typeof (MvcApplication).Assembly);

            // Data annotations validators
            container.Register(typeof (IValidationAttributeValidator<>), new[] {typeof (IValidationAttributeValidator<>).Assembly});

            // Loggers
            container.RegisterSingleton<ILogger, CompositeLogger>();
            container.RegisterCollection<ILogger>(new[] {typeof (NLogLogger)});
            container.RegisterSingleton(() => LogManager.GetLogger("NLog"));

            // Action filters
            container.RegisterCollection(typeof (IActionFilter<>), typeof (MvcApplication).Assembly);

            // Repository
            container.Register<IRepository, InMemoryRepository>();

            // Verify
            container.Verify();

            System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            System.Web.Mvc.GlobalFilters.Filters.Add(new ActionFilterDispatcher(type => (IEnumerable<IActionFilter>) container.GetAllInstances(type)));
        }
    }
}
