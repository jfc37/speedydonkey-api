using System.Collections.Generic;
using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Blocks
{
    [Binding]
    public class DeleteBlockSteps
    {
        [When(@"the block is deleted")]
        public void WhenTheBlockIsDeleted()
        {
            var blockId = ScenarioCache.GetId(ModelIdKeys.BlockKeyId);
            var response = ApiCaller.Delete<ActionReponse<BlockModel>>(Routes.GetById(Routes.Blocks, blockId));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the block can not be retrieved")]
        public void ThenTheBlockCanNotBeRetrieved()
        {
            var response = ApiCaller.Get<List<BlockModel>>(Routes.Blocks);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
