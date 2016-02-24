using System.Collections.Generic;
using System.Net;
using ActionHandlers;
using Contracts;
using Contracts.Blocks;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Blocks
{
    [Binding]
    public class DeleteBlockSteps
    {
        [When(@"the block is deleted")]
        public void WhenTheBlockIsDeleted()
        {
            var blockId = ScenarioCache.GetId(ModelIdKeys.BlockId);
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
