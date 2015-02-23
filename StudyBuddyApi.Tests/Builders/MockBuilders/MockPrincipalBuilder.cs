using System.Security.Principal;
using System.Threading;
using Common.Tests.Builders.MockBuilders;
using Moq;

namespace SpeedyDonkeyApi.Tests.Builders.MockBuilders
{
    public class MockPrincipalBuilder : MockBuilder<IPrincipal>
    {
        public MockPrincipalBuilder()
        {
            Thread.CurrentPrincipal = Mock.Object;   
        }

        public MockPrincipalBuilder WithLoggedOnUser()
        {
            Mock.Setup(x => x.Identity)
                .Returns(new GenericIdentity("1"));
            return this;
        }

        public MockPrincipalBuilder WithNoLoggedOnUser()
        {
            var mockedIdentity = new Mock<IIdentity>();
            mockedIdentity.Setup(x => x.IsAuthenticated)
                .Returns(false);
            Mock.Setup(x => x.Identity)
                .Returns(mockedIdentity.Object);
            return this;
        }
    }
}