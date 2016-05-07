using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts.Blocks;
using Contracts.Enrolment;
using Contracts.Users;
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

        [When(@"the user tries to enrol in the block")]
        public void WhenTheUserTriesToEnrolInTheBlock()
        {
            var response = ApiCaller.Post<ActionReponse<UserModel>>(new EnrolmentModel(ScenarioCache.GetId(ModelIdKeys.BlockId)),
                Routes.GetEnrolUserInBlock(ScenarioCache.GetUserId()));

            ScenarioCache.StoreActionResponse(response);
        }


        [When(@"blocks for enrolment is requested")]
        public void WhenBlocksForEnrolmentIsRequested()
        {
            var response = ApiCaller.Get<List<BlockForRegistrationModel>>(Routes.BlocksForEnrolment);

            ScenarioCache.StoreResponse(response);
        }

        [Then(@"there are blocks available for enrolment")]
        public void ThenThereAreBlocksAvailableForEnrolment()
        {
            var availableBlocks = ScenarioCache.GetResponse<List<BlockForRegistrationModel>>();
            Assert.IsNotEmpty(availableBlocks);

            availableBlocks.ForEach(x => Assert.Greater(x.SpacesAvailable, 0));
        }

        [Then(@"the user sees the block as already enrolled")]
        public void ThenTheUserSeesTheBlockAsAlreadyEnrolled()
        {
            var response = ApiCaller.Get<List<BlockForRegistrationModel>>(Routes.GetBlocksForEnrolment(2));

            response.Data.ForEach(x => Assert.IsTrue(x.IsAlreadyRegistered));
        }

        [Then(@"the number of spaces available has decreased")]
        public void ThenTheNumberOfSpacesAvailableHasDecreased()
        {
            var response = ApiCaller.Get<List<BlockForRegistrationModel>>(Routes.GetBlocksForEnrolment(2));

            response.Data.ForEach(x => Assert.Less(x.SpacesAvailable, x.ClassCapacity));
        }
        

        [Then(@"the user is enroled in the block")]
        public void ThenTheUserIsEnroledInTheBlock()
        {
            var response = ApiCaller.Get<BlockForRegistrationModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockId)));

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
