using System.Collections.Generic;
using Contracts.Events;
using IntegrationTests.Utilities;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.StandAloneEvents
{
    [Binding]
    public class ViewStandAloneEventSteps
    {
        [Given(@"a private stand alone event exists")]
        public void GivenAPrivateStandAloneEventExists()
        {
            var createSteps = new CreateStandAloneEventSteps();
            createSteps.GivenAValidStandAloneEventIsReadyToBeSubmitted();

            var standAloneEvent = ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEvent);
            standAloneEvent.IsPrivate = true;
            ScenarioCache.Store(ModelKeys.StandAloneEvent, standAloneEvent);

            createSteps.WhenTheStandAloneEventIsAttemptedToBeCreated();
            createSteps.ThenTheStandAloneEventCanBeRetrieved();
        }

        [Given(@"a stand alone event exists")]
        public void GivenAStandAloneEventExists()
        {
            var createSteps = new CreateStandAloneEventSteps();
            createSteps.GivenAValidStandAloneEventIsReadyToBeSubmitted();
            createSteps.WhenTheStandAloneEventIsAttemptedToBeCreated();
            createSteps.ThenTheStandAloneEventCanBeRetrieved();
        }

        [When(@"upcoming stand alone events are requested")]
        public void WhenUpcomingStatndAloneEventsAreRequested()
        {
            var response = ApiCaller.Get<List<StandAloneEventModel>>(Routes.StandAloneEventsForRegistration);

            ScenarioCache.StoreResponse(response);
        }

    }
}
