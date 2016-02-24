using System.Collections.Generic;
using System.Linq;
using System.Net;
using Contracts.Blocks;
using IntegrationTests.Steps.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Blocks
{
    [Binding]
    public class BlockEnrolmentSteps
    {
        [When(@"the user enrols in the block")]
        public void WhenTheUserEnrolsInTheBlock()
        {
            new CommonBlockSteps().GivenTheUserEnrolsInTheBlock();
        }

        [When(@"blocks for enrolment is requested")]
        public void WhenBlocksForEnrolmentIsRequested()
        {
            var response = ApiCaller.Get<List<BlockModel>>(Routes.BlocksForEnrolment);

            ScenarioCache.StoreResponse(response);
        }

        [Then(@"there are blocks available for enrolment")]
        public void ThenThereAreBlocksAvailableForEnrolment()
        {
            var availableBlocks = ScenarioCache.GetResponse<List<BlockModel>>();
            Assert.IsNotEmpty(availableBlocks);
        }

        [Then(@"the user is enroled in the block")]
        public void ThenTheUserIsEnroledInTheBlock()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var user = response.Data.EnroledStudents.Single();
            Assert.AreEqual(ScenarioCache.GetId(ModelIdKeys.UserId), user.Id);
        }

        [Then(@"the user has an item in their upcoming schedule")]
        public void ThenTheUserHasAnItemInTheirUpcomingSchedule()
        {
            var currentUserScheduleSteps = new UserScheduleSteps();
            currentUserScheduleSteps.WhenTheUserScheduleIsRetrieved();
            currentUserScheduleSteps.ThenTheUserSScheduleIsNotEmtpy();
        }

    }
}
