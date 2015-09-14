using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Common.Extensions;
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
            ScenarioCache.Store(ModelKeys.BlockModelKey, response.Data.ActionResult);
        }

        [When(@"the next block is generated")]
        public void WhenTheNextBlockIsGenerated()
        {
            WhenABlockIsGeneratedFromTheLevel();
        }

        [Then(@"block can be retrieved")]
        public void ThenBlockCanBeRetrieved()
        {
            var response = ApiCaller.Get<List<BlockModel>>(Routes.Blocks);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);
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
    }
}
