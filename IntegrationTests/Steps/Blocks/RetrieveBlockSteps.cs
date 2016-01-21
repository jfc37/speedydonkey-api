using System.Collections.Generic;
using Contracts;
using Contracts.Blocks;
using IntegrationTests.Utilities;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Blocks
{
    [Binding]
    public class RetrieveBlockSteps
    {
        [When(@"all blocks are retreived")]
        public void WhenAllBlocksAreRetreived()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.Blocks);
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }

        [When(@"a block search is performed")]
        public void WhenABlockSearchIsPerformed()
        {
            var response = ApiCaller.Get<List<BlockModel>>(Routes.GetSearch(Routes.Blocks, "id_=_1"));
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }

        [When(@"a block is retrieved by id")]
        public void WhenABlockIsRetrievedById()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetActionResponse<BlockModel>().Id));
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }

    }
}
