using System.Net;
using ActionHandlers;
using Contracts.Enrolment;
using Contracts.Users;
using IntegrationTests.Steps.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Blocks
{
    [Binding]
    public class CommonBlockSteps
    {
        [Given(@"a block exists")]
        public void GivenABlockExists()
        {
            var createBlockSteps = new CreateBlockSteps();
            createBlockSteps.GivenAValidBlockIsReadyToBeSubmitted();
            createBlockSteps.WhenTheBlockIsAttemptedToBeCreated();
            createBlockSteps.ThenBlockCanBeRetrieved();
        }

        [Given(@"an invite only block exists")]
        public void GivenAnInviteOnlyBlockExists()
        {
            var createBlockSteps = new CreateBlockSteps();
            createBlockSteps.GivenAValidBlockIsReadyToBeSubmitted();
            createBlockSteps.GivenTheBlockIsInviteOnly();
            createBlockSteps.WhenTheBlockIsAttemptedToBeCreated();
            createBlockSteps.ThenBlockCanBeRetrieved();
        }


        [Given(@"'(.*)' blocks exists")]
        public void GivenBlocksExists(int numberOfBlocks)
        {
            for (var blockNumber = 1; blockNumber <= numberOfBlocks; blockNumber++)
            {
                GivenABlockExists();
            }
        }

        [Given(@"the user enrols in the block")]
        public void GivenTheUserEnrolsInTheBlock()
        {
            var response = ApiCaller.Post<ActionReponse<UserModel>>(new EnrolmentModel(ScenarioCache.GetId(ModelIdKeys.BlockId)),
                Routes.GetEnrolUserInBlock(ScenarioCache.GetUserId()));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Given(@"'(.*)' users are enrols in the block")]
        public void GivenUsersAreEnrolsInTheBlock(int numberOfUsers)
        {
            for (int i = 1; i <= numberOfUsers; i++)
            {
                new CommonUserSteps().GivenAUserExists();
                GivenTheUserEnrolsInTheBlock();
            }
        }

        [Given(@"the user enrols in '(.*)' blocks")]
        public void GivenTheUserEnrolsInBlocks(int numberOfBlocks)
        {
            for (int blockId = 1; blockId <= numberOfBlocks; blockId++)
            {
                var response = ApiCaller.Post<ActionReponse<UserModel>>(new EnrolmentModel(blockId),
                    Routes.GetEnrolUserInBlock(ScenarioCache.GetUserId()));

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);   
            }

        }

    }
}
