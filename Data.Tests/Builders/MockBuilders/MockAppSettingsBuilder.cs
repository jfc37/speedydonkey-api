using Common;
using Common.Tests.Builders.MockBuilders;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockAppSettingsBuilder : MockBuilder<IAppSettings>
    {
        public MockAppSettingsBuilder WithAllSettings()
        {
            Mock.Setup(x => x.GetWebsiteUrl())
                .Returns("https://something.com");
            return this;
        }
    }
}
