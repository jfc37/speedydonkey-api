using System.Net.Http;
using Common.Tests.Builders.MockBuilders;
using Moq;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Tests.Builders.MockBuilders
{
    public class MockUrlConstructorBuilder : MockBuilder<IUrlConstructor>
    {
        public MockUrlConstructorBuilder WithUrlConstruction()
        {
            Mock.Setup(x => x.Construct(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<HttpRequestMessage>()))
                .Returns("http://blah.com");
            return this;
        }
    }
}