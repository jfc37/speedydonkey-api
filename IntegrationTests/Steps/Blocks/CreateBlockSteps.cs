using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Utilities;
using Models;
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

            var blockResponse = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, response.Data.ActionResult.Id));
            ScenarioCache.Store(ModelKeys.BlockModelKey, blockResponse.Data);
        }

        [When(@"the next block is generated")]
        public void WhenTheNextBlockIsGenerated()
        {
            WhenABlockIsGeneratedFromTheLevel();
        }

        [Then(@"block can be retrieved")]
        public void ThenBlockCanBeRetrieved()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
        }

        [Then(@"the first class of the second block is a week after the last class of the first block")]
        public void ThenTheFirstClassOfTheSecondBlockIsAWeekAfterTheLastClassOfTheFirstBlock()
        {
            var firstBlock = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, 1));
            var lastClassId = firstBlock.Data.Classes.Max(x => x.Id);
            var lastClassOfFirstBlock = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, lastClassId)).Data;

            var secondBlock = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, 2));
            var firstClassId = secondBlock.Data.Classes.Min(x => x.Id);
            var firstClassOfFirstBlock = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, firstClassId)).Data;

            Assert.AreEqual(lastClassOfFirstBlock.StartTime.AddWeeks(1).AddDays(1), firstClassOfFirstBlock.StartTime);
        }

        [Then(@"classes are created for the block")]
        public void ThenClassesAreCreatedForTheBlock()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));
            var block = response.Data;

            Assert.IsNotEmpty(block.Classes);

            foreach (var classModel in block.Classes)
            {
                Assert.AreEqual(classModel.StartTime.AddHours(1), classModel.EndTime);
            }
        }

        [Then(@"the correct number of classes are created")]
        public void ThenTheCorrectNumberOfClassesAreCreated()
        {
            var level = ScenarioCache.Get<LevelModel>(ModelKeys.LevelModelKey);
            var block = ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey);

            Assert.AreEqual(level.ClassesInBlock, block.Classes.Count);
        }

    }
}
