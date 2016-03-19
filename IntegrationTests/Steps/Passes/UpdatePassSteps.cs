using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts.Passes;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Passes
{
    [Binding]
    public class UpdatePassSteps
    {
        private const string _updatedPassKey = "UpdatedPassKey";

        [Given(@"the pass needs to be changed")]
        public void GivenThePassNeedsToBeChanged()
        {
            var existingPass = ScenarioCache.Get<PassModel>(ModelKeys.Pass);

            existingPass.StartDate = existingPass.StartDate.AddMonths(1);
            existingPass.EndDate = existingPass.EndDate.AddMonths(1);

            ScenarioCache.Store(_updatedPassKey, existingPass);
        }

        [When(@"the pass is updated")]
        public void WhenThePassIsUpdated()
        {
            var updatedPass = ScenarioCache.Get<PassModel>(_updatedPassKey);

            var restResponse = ApiCaller.Put<ActionReponse<PassModel>>(updatedPass, Routes.GetById("passes", updatedPass.Id));

            ScenarioCache.StoreResponse(restResponse);
        }

        [Then(@"the pass details are updated")]
        public void ThenThePassDetailsAreUpdated()
        {
            var response = ApiCaller.Get<List<PassModel>>(Routes.GetUserPasses(ScenarioCache.GetId(ModelIdKeys.UserId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);

            var retrievedPass = response.Data.Single();
            var updatedPass = ScenarioCache.Get<PassModel>(_updatedPassKey);

            VerifyClipPassProperties.Verify(updatedPass, retrievedPass);
        }

    }
}
