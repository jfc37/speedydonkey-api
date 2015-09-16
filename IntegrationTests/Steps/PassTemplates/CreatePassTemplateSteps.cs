using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.PassTemplates
{
    [Binding]
    public class CreatePassTemplateSteps
    {
        [Given(@"a valid pass template is ready to be submitted")]
        public void GivenAValidPassTemplateIsReadyToBeSubmitted()
        {
            var passTemplate = new PassTemplateModel
            {
                AvailableForPurchase = true,
                ClassesValidFor = 6,
                Cost = 100,
                Description = "6 Week Pass",
                PassType = PassType.Clip.ToString(),
                WeeksValidFor = 9
            };

            ScenarioCache.Store(ModelKeys.PassTemplateModelKey, passTemplate);
        }

        [Given(@"an invalid pass template is ready to be submitted")]
        public void GivenAnInvalidPassTemplateIsReadyToBeSubmitted()
        {
            var passTemplate = new PassTemplateModel
            {
                AvailableForPurchase = true,
                ClassesValidFor = 6,
                Cost = 100,
                PassType = PassType.Clip.ToString(),
                WeeksValidFor = 9
            };

            ScenarioCache.Store(ModelKeys.PassTemplateModelKey, passTemplate);
        }

        [When(@"the pass template is attempted to be created")]
        public void WhenThePassTemplateIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<PassTemplateModel>>(ScenarioCache.Get<PassTemplateModel>(ModelKeys.PassTemplateModelKey), Routes.PassTemplate);
            ScenarioCache.StoreActionResponse(response);
            ScenarioCache.Store(ModelIdKeys.PassTemplateKeyId, response.Data.ActionResult.Id);
        }

        [Then(@"pass template can be retrieved")]
        public void ThenPassTemplateCanBeRetrieved()
        {
            var passTemplateId = ScenarioCache.GetId(ModelIdKeys.PassTemplateKeyId);
            Assert.Greater(passTemplateId, 0);

            var response = ApiCaller.Get<PassTemplateModel>(Routes.GetPassTemplateById(passTemplateId));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            new VerifyPassTemplateProperties(ScenarioCache.Get<PassTemplateModel>(ModelKeys.PassTemplateModelKey), response.Data)
                .Verify();
        }

        [Then(@"pass template can not be retrieved")]
        public void ThenPassTemplateCanNotBeRetrieved()
        {
            var response = ApiCaller.Get<PassTemplateModel>(Routes.PassTemplate);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
