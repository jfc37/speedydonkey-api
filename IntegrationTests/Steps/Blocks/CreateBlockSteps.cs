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
    public class CreateBlockSteps
    {
        [When(@"a block is generated from the level")]
        public void WhenABlockIsGeneratedFromTheLevel()
        {
            var response = ApiCaller.Post<ActionReponse<BlockModel>>(Routes.GetCreateBlock(ScenarioCache.GetId(ModelIdKeys.LevelIdKey)));

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            ScenarioCache.StoreActionResponse(response);
            ScenarioCache.Store(ModelIdKeys.BlockKeyId, response.Data.ActionResult.Id);
        }

        [Then(@"block can be retrieved")]
        public void ThenBlockCanBeRetrieved()
        {
            var response = ApiCaller.Get<List<BlockModel>>(Routes.Blocks);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);
        }

    }
}
