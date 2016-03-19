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
            var standAloneEvent = ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEvent);

            standAloneEvent.IsPrivate = true;

            ScenarioCache.Store(ModelKeys.StandAloneEvent, standAloneEvent);
        }

        [When(@"the stand alone event is updated")]
        public void WhenTheStandAloneEventIsUpdated()
        {
            var response = ApiCaller.Put<ActionReponse<StandAloneEventModel>>(ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEvent), Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventId)));

            ScenarioCache.StoreActionResponse(response);
        }


        [Then(@"the stand alone event is now private")]
        public void ThenTheStandAloneEventIsNowPrivate()
        {
            var response = ApiCaller.Get<StandAloneEventModel>(Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);

            Assert.IsTrue(response.Data.IsPrivate);
        }


    }
}
