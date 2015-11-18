using System.Collections.Generic;
using System.Linq;
using System.Net;
using Common.Extensions;
using IntegrationTests.Utilities;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class GetClaimsSteps
    {
        private static string ClaimsResponseKey = "userClaimsResponse";
        [Given(@"an admin user")]
        public void GivenAnAdminUser()
        {
        }

        [When(@"their claims are retrieved")]
        public void WhenTheirClaimsAreRetrieved()
        {
            var response = ApiCaller.Get<List<string>>(Routes.GetUserClaims(ScenarioCache.GetUserId()));

            ScenarioCache.Store(ClaimsResponseKey, response);
        }

        [Then(@"their claims are empty")]
        public void ThenTheirClaimsAreEmpty()
        {
            var response = ScenarioCache.Get<RestResponse<List<string>>>(ClaimsResponseKey);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Then(@"their claims are '(.*)'")]
        public void ThenTheirClaimsAre(string expectedClaims)
        {
            ThenTheirClaimsAre(expectedClaims.Split(',').ToList());
        }

        [Then(@"their claims are '(.*)'")]
        public void ThenTheirClaimsAre(List<string> expectedClaims)
        {
            var response = ScenarioCache.Get<RestResponse<List<string>>>(ClaimsResponseKey);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(expectedClaims.Select(x => x.ToLower()).HasSameItems(response.Data.Select(x => x.ToLower())));
        }
    }
}
