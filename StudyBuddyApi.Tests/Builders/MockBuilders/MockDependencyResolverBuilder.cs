using System.Web.Http;
using System.Web.Http.Dependencies;
using Common.Tests.Builders.MockBuilders;

namespace SpeedyDonkeyApi.Tests.Builders.MockBuilders
{
    public class MockDependencyResolverBuilder : MockBuilder<IDependencyResolver>
    {
        public MockDependencyResolverBuilder()
        {
            GlobalConfiguration.Configuration.DependencyResolver = BuildObject();
        }

        public MockDependencyResolverBuilder WithService<T>(T service)
        {
            Mock.Setup(x => x.GetService(typeof (T)))
                .Returns(service);
            return this;
        }
    }
}