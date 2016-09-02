using Reusables.Cqrs;
using Reusables.EventSourcing;
using SimpleInjector;
using Xunit.Abstractions;

namespace MailingServiceDemo.Tests.AsyncTests
{
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
            //container.RegisterDecorator(typeof (IAsyncEventSubscriber<>), typeof (AsyncEventProcrastinator<>));

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
}