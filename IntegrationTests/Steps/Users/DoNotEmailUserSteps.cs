using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class DoNotEmailUserSteps
    {
        [Given(@"the user does not want to receive emails")]
        public void GivenTheUserDoesNotWantToReceiveEmails()
        {
            var response = ApiCaller.Post<ActionReponse<UserModel>>(
                Routes.GetDoNotEmailUser(ScenarioCache.GetUserId()));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Given(@"the user does want to receive emails")]
        public void GivenTheUserDoesWantToReceiveEmails()
        {
            var response = ApiCaller.Delete<ActionReponse<UserModel>>(
                Routes.GetDoNotEmailUser(ScenarioCache.GetUserId()));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
