using System.Net;
using ActionHandlers;
using Contracts;
using Contracts.Blocks;
using Contracts.Classes;
using IntegrationTests.Utilities;
using NUnit.Framework;
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

            ScenarioCache.Store(ModelIdKeys.ClassId, classModel.Id);
            ScenarioCache.Store(ModelKeys.Class, classModel);
        }

        [When(@"the class is updated")]
        public void WhenTheClassIsUpdated()
        {
            var response = ApiCaller.Put<ActionReponse<ClassModel>>(ScenarioCache.Get<ClassModel>(ModelKeys.Class),
                Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassId)));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the class's start and end time is updated")]
        public void ThenTheClassSStartAndEndTimeIsUpdated()
        {
            var classModel = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassId))).Data;

            var expectedClassModel = ScenarioCache.Get<ClassModel>(ModelKeys.Class);
            Assert.AreEqual(expectedClassModel.StartTime, classModel.StartTime);
            Assert.AreEqual(expectedClassModel.EndTime, classModel.EndTime);
        }

        [Then(@"the blocks start and end time is unchanged")]
        public void ThenTheBlocksStartAndEndTimeIsUnchanged()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockId)));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var block = response.Data;

            var expectedBlock = ScenarioCache.Get<BlockModel>(ModelKeys.Block);
            Assert.AreEqual(expectedBlock.StartDate, block.StartDate);
            Assert.AreEqual(expectedBlock.EndDate, block.EndDate);
        }
    }
}
