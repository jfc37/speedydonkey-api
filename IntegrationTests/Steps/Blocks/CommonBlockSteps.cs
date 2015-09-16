using System.Net;
using ActionHandlers;
using IntegrationTests.Steps.Levels;
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
            new CommonLevelSteps().GivenALevelExists();
            var createBlockSteps = new CreateBlockSteps();
            createBlockSteps.WhenABlockIsGeneratedFromTheLevel();
            createBlockSteps.ThenBlockCanBeRetrieved();
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
