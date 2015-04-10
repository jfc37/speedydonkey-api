using Common;
using Common.Tests.Builders.MockBuilders;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockAppSettingsBuilder : MockBuilder<IAppSettings>
    {
        public MockAppSettingsBuilder WithAllSettings()
        {
            Mock.Setup(x => x.GetWebsiteUrl())
                .Returns("https://something.com");
            Mock.Setup(x => x.GetSetting(It.IsAny<AppSettingKey>()))
                .Returns("blah x2");
            return this;
        }

        public MockAppSettingsBuilder WithSetting(AppSettingKey appSettingKey, string value)
        {
            Mock.Setup(x => x.GetSetting(appSettingKey))
                .Returns(value);
            return this;
        }
    }
}
