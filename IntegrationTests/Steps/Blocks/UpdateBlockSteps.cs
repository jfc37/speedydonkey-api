using System.Linq;
using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Blocks
{
    [Binding]
    public class UpdateBlockSteps
    {
        [Given(@"the day the block is on needs to change")]
        public void GivenTheDayTheBlockIsOnNeedsToChange()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, 1));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var block = response.Data;
            block.StartDate = block.StartDate.AddDays(2);
            block.EndDate = block.EndDate.AddDays(2);

            ScenarioCache.Store(ModelIdKeys.BlockKeyId, block.Id);
            ScenarioCache.Store(ModelKeys.BlockModelKey, block);
        }

        [When(@"the block is updated")]
        public void WhenTheBlockIsUpdated()
        {
            var response = ApiCaller.Put<ActionReponse<BlockModel>>(ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey),
                Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the blocks start and end time is updated")]
        public void ThenTheBlocksStartAndEndTimeIsUpdated()
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId))).Data;

            var expectedBlock = ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey);
            Assert.AreEqual(expectedBlock.StartDate, block.StartDate);
            Assert.AreEqual(expectedBlock.EndDate, block.EndDate);

            ScenarioCache.Store(ModelKeys.BlockModelKey, block);
        }

        [Given(@"the day the block is on has been updated")]
        public void GivenTheDayTheBlockIsOnHasBeenUpdated()
        {
            GivenTheDayTheBlockIsOnNeedsToChange();
            WhenTheBlockIsUpdated();
        }


        [Then(@"the block's classes start and end time is updated")]
        public void ThenTheBlockSClassesStartAndEndTimeIsUpdated()
        {
            var block = ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey);
            var level = ScenarioCache.Get<LevelModel>(ModelKeys.LevelModelKey);

            var expectedStartTime = block.StartDate;
            var expectedEndTime = block.StartDate.AddMinutes(level.ClassMinutes);

            foreach (var theClass in block.Classes.OrderBy(x => x.Id))
            {
                Assert.AreEqual(expectedStartTime, theClass.StartTime);
                Assert.AreEqual(expectedEndTime, theClass.EndTime);

                expectedStartTime = expectedStartTime.AddDays(7);
                expectedEndTime = expectedEndTime.AddDays(7);
            }
        }

    }
}
