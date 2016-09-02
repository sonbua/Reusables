using System;
using System.Linq;
using System.Threading.Tasks;
using MailingServiceDemo.Command;
using MailingServiceDemo.CompositionRoot;
using MailingServiceDemo.Model;
using MailingServiceDemo.Tests.SyncTests;
using Reusables.Cqrs;
using Xunit;
using Xunit.Abstractions;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class RequestDispatcherAsyncTest
    {
        private readonly IServiceProvider _container;

        public RequestDispatcherAsyncTest(ITestOutputHelper testOutputHelper)
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
}
