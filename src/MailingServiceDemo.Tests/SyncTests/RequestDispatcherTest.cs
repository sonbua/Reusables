using System;
using MailingServiceDemo.Command;
using MailingServiceDemo.CompositionRoot;
using MailingServiceDemo.Model;
using Reusables.Cqrs;
using Xunit;
using Xunit.Abstractions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class RequestDispatcherTest
    {
        private readonly IServiceProvider _container;

        public RequestDispatcherTest(ITestOutputHelper testOutputHelper)
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
}
