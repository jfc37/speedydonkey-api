using System;
using System.Linq;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Steps.Teachers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Blocks
{
    [Binding]
    public class CreateBlockSteps
    {
        [Given(@"a valid block is ready to be submitted")]
        public void GivenAValidBlockIsReadyToBeSubmitted()
        {
            new CommonTeacherSteps().GivenAnExistingUserIsATeacher();

            var startDate = DateTime.Now.Date.AddDays(2);
            startDate = startDate.AddHours(11);
            var startDateWithOffset = new DateTimeOffset(startDate);
            var block = new BlockModel
            {
                MinutesPerClass = 60,
                NumberOfClasses = 6,
                Name = "Charleston Level 1",
                StartDate = startDateWithOffset,
                Teachers = new TeacherModel { Id = ScenarioCache.GetTeacherId() }.PutIntoList()
            };

            ScenarioCache.Store(ModelKeys.BlockModelKey, block);
        }

        [When(@"the block is attempted to be created")]
        public void WhenTheBlockIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<BlockModel>>(ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey), Routes.Blocks);
            ScenarioCache.StoreActionResponse(response);
            ScenarioCache.Store(ModelIdKeys.BlockKeyId, response.Data.ActionResult.Id);
        }

        [When(@"the next block is generated")]
        public void WhenTheNextBlockIsGenerated()
        {
            var response = ApiCaller.Post<ActionReponse<BlockModel>>(Routes.GetCreateNextBlock(ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            ScenarioCache.StoreActionResponse(response);
            ScenarioCache.Store(ModelIdKeys.BlockKeyId, response.Data.ActionResult.Id);
        }

        [Then(@"block can be retrieved")]
        public void ThenBlockCanBeRetrieved()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
        }

        [Then(@"the blocks dates are in utc")]
        public void ThenTheBlocksDatesAreInUtc()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));

            var originalBlock = ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey);
            Assert.AreNotEqual(originalBlock.StartDate.Offset, response.Data.StartDate.Offset);
            Assert.AreEqual(originalBlock.StartDate.ToUniversalTime(), response.Data.StartDate.ToUniversalTime());
            Assert.AreEqual(originalBlock.StartDate.ToLocalTime(), response.Data.StartDate.ToLocalTime());
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

            Assert.AreEqual(lastClassOfFirstBlock.StartTime.AddWeeks(1), firstClassOfFirstBlock.StartTime);
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
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));
            var block = response.Data;

            var expectedBlock = ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey);

            Assert.AreEqual(expectedBlock.NumberOfClasses, block.Classes.Count);
        }

    }
}
