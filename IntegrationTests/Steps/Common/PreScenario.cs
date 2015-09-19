using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using RestSharp;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Common
{
    [Binding]
    public class PreScenario
    {
        [BeforeScenario]
        public void SetupSystem()
        {
            SimpleJson.CurrentJsonSerializerStrategy = new SnakeJsonSerializerStrategy();
            ResetDatabase();
            CreateAdminUser();
        }

        private static void ResetDatabase()
        {
            ApiCaller.Delete<bool>(Routes.Database);
        }

        private void CreateAdminUser()
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
