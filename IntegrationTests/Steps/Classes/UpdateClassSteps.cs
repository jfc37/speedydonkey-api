using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Classes
{
    [Binding]
    public class UpdateClassSteps
    {
        [Given(@"a class time needs to change")]
        public void GivenAClassTimeNeedsToChange()
        {
            var response = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, 1));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var classModel = response.Data;
            classModel.StartTime = classModel.StartTime.AddHours(2);
            classModel.EndTime = classModel.EndTime.AddHours(3);

            ScenarioCache.Store(ModelIdKeys.ClassKeyId, classModel.Id);
            ScenarioCache.Store(ModelKeys.ClassModelKey, classModel);
        }

        [When(@"the class is updated")]
        public void WhenTheClassIsUpdated()
        {
            var response = ApiCaller.Put<ActionReponse<ClassModel>>(ScenarioCache.Get<ClassModel>(ModelKeys.ClassModelKey),
                Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassKeyId)));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the class's start and end time is updated")]
        public void ThenTheClassSStartAndEndTimeIsUpdated()
        {
            var classModel = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassKeyId))).Data;

            var expectedClassModel = ScenarioCache.Get<ClassModel>(ModelKeys.ClassModelKey);
            Assert.AreEqual(expectedClassModel.StartTime, classModel.StartTime);
            Assert.AreEqual(expectedClassModel.EndTime, classModel.EndTime);
        }

        [Then(@"the blocks start and end time is unchanged")]
        public void ThenTheBlocksStartAndEndTimeIsUnchanged()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var block = response.Data;

            var expectedBlock = ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey);
            Assert.AreEqual(expectedBlock.StartDate, block.StartDate);
            Assert.AreEqual(expectedBlock.EndDate, block.EndDate);
        }
    }
}
