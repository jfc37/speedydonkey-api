using Auth0;
using Common;
using IntegrationTests.Utilities;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Common
{
    [Binding]
    public static class PreScenario
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var appSettings = new AppSettings();
            var client = new Client(
                clientID: appSettings.GetSetting(AppSettingKey.AuthZeroClientId),
                clientSecret: appSettings.GetSetting(AppSettingKey.AuthZeroClientSecret),
                domain: appSettings.GetSetting(AppSettingKey.AuthZeroDomain)
                );
            var tokenResult = client.LoginUser("placid.joe@gmail.com", "password", "speedydonkeydb");
            ApiCaller.IdJwt = tokenResult.IdToken;
        }

        [BeforeScenario]
        public static void SetupSystem()
        {
            ResetDatabase();
            ScenarioCache.Store(ModelIdKeys.UserId, 1);
        }

        private static void ResetDatabase()
        {
            ApiCaller.Delete<bool>(Routes.Database);
        }
    }
}
