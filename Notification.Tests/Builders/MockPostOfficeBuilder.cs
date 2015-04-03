using Common.Tests.Builders.MockBuilders;
using Moq;
using Notification.Notifications;

namespace Notification.Tests.Builders
{
    public class MockPostOfficeBuilder : MockBuilder<IPostOffice>
    {
        public MockPostOfficeBuilder WithSending()
        {
            Mock.Setup(x => x.Send(It.IsAny<INotification>()));
            return this;
        }
    }
}
