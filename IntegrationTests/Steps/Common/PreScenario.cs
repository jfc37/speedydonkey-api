using System.Collections.Generic;
using System.Net;
using Auth0;
using Common;
using Contracts.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Common
{
    [Binding]
    public class CommonSteps
    {
        [Then(@"the request is successful")]
        public void ThenTheRequestIsSuccessful()
        {
            var httpStatusCode = ScenarioCache.GetResponseStatus();
            Assert.Contains(httpStatusCode, new[] { HttpStatusCode.OK, HttpStatusCode.Created });
        }
    }

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
            var tokenResult = client.LoginUser("placid.joe@gmail.com", "password", "Username-Password-Authentication");
            ApiCaller.IdJwt = tokenResult.IdToken;

        }

        [BeforeScenario]
        public static void SetupSystem()
        {
            ResetDatabase();
            ScenarioCache.Store(ModelIdKeys.UserId, 1);
            ScenarioCache.Store(ModelIdKeys.StudentIds, new List<int>());
            ScenarioCache.Store(ModelIdKeys.BlockIds, new List<int>());
        }

        private static void ResetDatabase()
        {
            ApiCaller.Delete<bool>(Routes.Database);

            ApiCaller.Post<object>(new AuthZeroUserModel("auth0|56e641bb5d3ca9ae1853a9d2"), $"{Routes.Users}/auth0");
        }
    }
}
