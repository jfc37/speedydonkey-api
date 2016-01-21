using System.Net;
using ActionHandlers;
using Contracts.Events;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.StandAloneEvents
{
    [Binding]
    public class UpdateStandAloneEventSteps
    {
        [Given(@"the stand alone event needs to be changed to private")]
        public void GivenTheStandAloneEventNeedsToBeChangedToPrivate()
        {
            var standAloneEvent = ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEventKey);

            standAloneEvent.IsPrivate = true;

            ScenarioCache.Store(ModelKeys.StandAloneEventKey, standAloneEvent);
        }

        [When(@"the stand alone event is updated")]
        public void WhenTheStandAloneEventIsUpdated()
        {
            var response = ApiCaller.Put<ActionReponse<StandAloneEventModel>>(ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEventKey), Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId)));

            ScenarioCache.StoreActionResponse(response);
        }


        [Then(@"the stand alone event is now private")]
        public void ThenTheStandAloneEventIsNowPrivate()
        {
            var response = ApiCaller.Get<StandAloneEventModel>(Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);

            Assert.IsTrue(response.Data.IsPrivate);
        }


    }
}
