using Common.Tests.Builders.MockBuilders;
using Models;
using Moq;
using Notification.NotificationHandlers;
using Notification.Notifications;
using NUnit.Framework;

namespace Notification.Tests
{
    [TestFixture]
    public class UserRegisteredHandlerTests
    {
        private MockMailManBuilder _mailManBuilder;
        private UserRegistered _notification;

        [SetUp]
        public void Setup()
        {
            _notification = new UserRegistered(new User(), "blah");
            _mailManBuilder = new MockMailManBuilder()
                .WithSending();
        }

        private NotificationHandler<UserRegistered> GetHandler()
        {
            return new NotificationHandler<UserRegistered>(_mailManBuilder.BuildObject());
        }
        private void PerformAction()
        {
            GetHandler().Handle(_notification);
        }

        [Test]
        public void It_should_send_an_email()
        {
            PerformAction();

            _mailManBuilder.Mock.Verify(x => x.Send(_notification));
        }
    }

    public class MockMailManBuilder : MockBuilder<IMailMan>
    {
        public MockMailManBuilder WithSending()
        {
            Mock.Setup(x => x.Send(It.IsAny<INotification>()));
            return this;
        }
    }
}
