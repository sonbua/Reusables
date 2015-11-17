using System;
using System.Linq;
using System.Net.Mail;
using MailingServiceDemo.Command;
using MailingServiceDemo.CompositionRoot;
using MailingServiceDemo.Event;
using MailingServiceDemo.ReadModel;
using Ploeh.AutoFixture.Xunit2;
using Reusables.Cqrs;
using Reusables.EventSourcing;
using SimpleInjector;
using Xunit;

namespace MailingServiceDemo.Tests
{
    public class Tests
    {
        private readonly Container _container;

        public Tests()
        {
            _container = DependencyResolverConfig.RegisterDependencies();
        }

        [Theory]
        [AutoData]
        public void ShouldHaveSendMailCommandHandler()
        {
            // arrange
            var sendMail = new SendMail {Messages = new[] {new MailMessage()}};
            var dispatcher = _container.GetInstance<IRequestDispatcher>();

            // act
            dispatcher.DispatchCommand(sendMail);

            // assert
            Assert.True(sendMail.Id != Guid.Empty);
        }

        [Fact]
        public void ShouldHaveMailRequestReceivedEventSubscriber()
        {
            // arrange

            // act

            // assert
            Assert.NotEmpty(_container.GetAllInstances<IEventSubscriber<MailRequestReceived>>());
        }

        [Fact]
        public void ShouldSaveAllMailMessagesToDatabase()
        {
            // arrange
            var sendMail = new SendMail {Messages = new[] {new MailMessage(), new MailMessage()}};
            var dispatcher = _container.GetInstance<IRequestDispatcher>();
            var viewModelDatabase = _container.GetInstance<IViewModelDatabase>();

            // act
            dispatcher.DispatchCommand(sendMail);

            // assert
            Assert.Equal(2, viewModelDatabase.Set<OutboxMessage>().Count());
        }
    }
}
