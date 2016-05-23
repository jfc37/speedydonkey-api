using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts;
using Contracts.Blocks;
using Contracts.Teachers;
using IntegrationTests.Steps.Common;
using IntegrationTests.Steps.Teachers;
using IntegrationTests.Utilities;
using NUnit.Framework;
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

            ScenarioCache.Store(ModelIdKeys.BlockId, block.Id);
            ScenarioCache.Store(ModelKeys.Block, block);
        }

        [Given(@"the block needs to change to invite only")]
        public void GivenTheBlockNeedsToChangeToInviteOnly()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, 1));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var block = response.Data;
            block.IsInviteOnly = true;

            ScenarioCache.Store(ModelIdKeys.BlockId, block.Id);
            ScenarioCache.Store(ModelKeys.Block, block);
        }

        [Given(@"the block has '(.*)' teacher")]
        public void GivenTheBlockHasTeacher(int numberOfTeachers)
        {
            var teachers = Enumerable.Range(1, numberOfTeachers)
                .Select(x => CreateNewTeacher())
                .ToList();
            
            SetTeachersForBlock(teachers);
        }

        public void SetTeachersForBlock(List<TeacherModel> teachers)
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, 1));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var block = response.Data;
            block.Teachers = teachers;

            ScenarioCache.Store(ModelIdKeys.BlockId, block.Id);
            ScenarioCache.Store(ModelKeys.Block, block);

            WhenTheBlockIsUpdated();
            new CommonSteps().ThenTheRequestIsSuccessful();
        }

        private TeacherModel CreateNewTeacher()
        {
            new CommonTeacherSteps().GivenAnExistingUserIsATeacher();
            var id = ScenarioCache.GetTeacherId();

            return new TeacherModel(id);
        }


        [Given(@"the block needs to change from invite only")]
        public void GivenTheBlockNeedsToChangeFromInviteOnly()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, 1));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var block = response.Data;
            block.IsInviteOnly = false;

            ScenarioCache.Store(ModelIdKeys.BlockId, block.Id);
            ScenarioCache.Store(ModelKeys.Block, block);
        }

        [Given(@"the block class capacity changes to '(.*)'")]
        public void GivenTheBlockClassCapacityChangesTo(int classCapacity)
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, 1));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var block = response.Data;
            block.ClassCapacity = classCapacity;

            ScenarioCache.Store(ModelIdKeys.BlockId, block.Id);
            ScenarioCache.Store(ModelKeys.Block, block);
        }
        
        [When(@"the block is updated")]
        public void WhenTheBlockIsUpdated()
        {
            var response = ApiCaller.Put<ActionReponse<BlockModel>>(ScenarioCache.Get<BlockModel>(ModelKeys.Block),
                Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockId)));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the block class capacity is '(.*)'")]
        public void ThenTheBlockClassCapacityIs(int classCapacity)
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockId))).Data;

            Assert.AreEqual(classCapacity, block.ClassCapacity);
            ScenarioCache.Store(ModelKeys.Block, block);
        }

        [Then(@"the blocks start and end time is updated")]
        public void ThenTheBlocksStartAndEndTimeIsUpdated()
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockId))).Data;

            var expectedBlock = ScenarioCache.Get<BlockModel>(ModelKeys.Block);
            Assert.AreEqual(expectedBlock.StartDate, block.StartDate);
            Assert.AreEqual(expectedBlock.EndDate, block.EndDate);

            ScenarioCache.Store(ModelKeys.Block, block);
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
            var block = ScenarioCache.Get<BlockModel>(ModelKeys.Block);

            var expectedStartTime = block.StartDate;
            var expectedEndTime = block.StartDate.AddMinutes(block.MinutesPerClass);

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
