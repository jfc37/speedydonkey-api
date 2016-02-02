using ActionHandlers;
using Contracts;
using Contracts.Blocks;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Rooms
{
    [Binding]
    public class UnassignRoomFromBlockSteps
    {
        [When(@"the block room unassignment is requested")]
        public void WhenTheBlockRoomUnassignmentIsRequested()
        {
            var blockId = ScenarioCache.GetId(ModelIdKeys.Block);
            var response = ApiCaller.Delete<ActionReponse<BlockModel>>(Routes.GetBlockRoom(blockId));
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the block details does not have the room")]
        public void ThenTheBlockDetailsDoesNotHaveTheRoom()
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.Block))).Data;

            Assert.IsNull(block.Room);
        }
    }
}
