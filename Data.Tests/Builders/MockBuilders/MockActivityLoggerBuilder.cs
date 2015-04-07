using Common.Tests.Builders.MockBuilders;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockActivityLoggerBuilder : MockBuilder<IActivityLogger>
    {
        public MockActivityLoggerBuilder WithLogging()
        {
            Mock.Setup(x => x.Log(It.IsAny<ActivityGroup>(), It.IsAny<ActivityType>(), It.IsAny<string>()));
            return this;
        }
    }
}
