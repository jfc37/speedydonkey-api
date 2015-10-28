using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
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

        [Given(@"'(.*)' blocks exists")]
        public void GivenBlocksExists(int numberOfBlocks)
        {
            for (int blockNumber = 1; blockNumber <= numberOfBlocks; blockNumber++)
            {
                GivenABlockExists();
            }
        }

        [Given(@"the user enrols in the block")]
        public void GivenTheUserEnrolsInTheBlock()
        {
            var response = ApiCaller.Post<ActionReponse<UserModel>>(new EnrolmentModel(ScenarioCache.GetId(ModelIdKeys.BlockKeyId)),
                Routes.GetEnrolUserInBlock(ScenarioCache.GetUserId()));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
