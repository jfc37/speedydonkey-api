using System.Net;
using ActionHandlers;
using Contracts.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class TermsAndConditionsSteps
    {
        [When(@"the user agrees to the terms and conditions")]
        public void WhenTheUserAgreesToTheTermsAndConditions()
        {
            var userResponse = ApiCaller.Post<ActionReponse<UserModel>>(Routes.TermsAndConditions);

            ScenarioCache.StoreActionResponse(userResponse);
        }

        [Then(@"the users term and conditions agreement is recorded")]
        public void ThenTheUsersTermAndConditionsAgreementIsRecorded()
        {
            var userId = ScenarioCache.GetUserId();

            var response = ApiCaller.Get<UserModel>(Routes.GetUserById(userId));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.Data.AgreesToTerms);
        }

    }
}
