using System.Net;
using ActionHandlers;
using Auth0;
using Common;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
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
            var tokenResult = client.LoginUser("placid.joe@gmail.com", "password1", "speedydonkeydb");
            ApiCaller.IdJwt = tokenResult.IdToken;
        }

        [BeforeScenario]
        public static void SetupSystem()
        {
            ResetDatabase();
            ScenarioCache.StoreUserId(1);
        }

        private static void ResetDatabase()
        {
            ApiCaller.Delete<bool>(Routes.Database);
        }

        private static void CreateAdminUser()
        {
            var userRequest = new UserModel();
            userRequest.Surname = "admin";
            userRequest.Email = "joseph@fullswing.co.nz";
            userRequest.Password = "password";
            userRequest.FirstName = "admin";

            var userResponse = ApiCaller.Post<ActionReponse<UserModel>>(userRequest, Routes.Users);

            Assert.AreEqual(HttpStatusCode.Created, userResponse.StatusCode);
        }
    }
}
