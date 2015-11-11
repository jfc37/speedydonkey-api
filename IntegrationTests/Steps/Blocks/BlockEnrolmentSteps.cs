using System.Linq;
using System.Net;
using IntegrationTests.Steps.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
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

        [Then(@"the user is enroled in the block")]
        public void ThenTheUserIsEnroledInTheBlock()
        {
            var response = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var user = response.Data.EnroledStudents.Single();
            Assert.AreEqual(ScenarioCache.GetId(ModelIdKeys.UserIdKey), user.Id);
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
