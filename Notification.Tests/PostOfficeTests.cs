using System;
using Common.Tests.Builders;
using Notification.NotificationHandlers;
using Notification.Notifications;
using NUnit.Framework;

namespace Notification.Tests
{
    [TestFixture]
    public class PostOfficeTests
    {
        protected LifetimeScopeBuilder LifetimeScopeBuilder;

        [SetUp]
        public virtual void Setup()
        {
            LifetimeScopeBuilder = new LifetimeScopeBuilder()
                .WithNotificationHandlersRegistered();
        }

        private IPostOffice GetPostOffice()
        {
            return new PostOffice(LifetimeScopeBuilder.Build());
        }

        protected void PerformAction<T>(T notification) where T : INotification
        {
            GetPostOffice().Send(notification);
        }
    }

    public class GivenANotificationThatHasAHandler : PostOfficeTests
    {
        private TestNotification _notification;
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _notification = new TestNotification();
        }

        [Test]
        public void Then_it_should_call_the_handler()
        {
            Assert.DoesNotThrow(() => PerformAction<TestNotification>(_notification));
        }
    }

    public class GivenANotificationThatDoesNotHaveAHandler : PostOfficeTests
    {
        private TestNotification _notification;
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _notification = new TestNotification();
            LifetimeScopeBuilder.WithNothingRegistered();
        }

        [Test]
        public void Then_it_should_throw_an_argument_exception()
        {
            Assert.Throws<ArgumentException>(() => PerformAction<TestNotification>(_notification));
        }
    }

    internal class TestNotification : INotification
    {
        public string EmailTo { get; private set; }
        public string Subject { get; private set; }
        public string EmailBody { get; set; }
    }

    internal class TestNotificationHandler : INotificationHandler<TestNotification>
    {
        public void Handle(TestNotification notification)
        {
            
        }
    }
}
