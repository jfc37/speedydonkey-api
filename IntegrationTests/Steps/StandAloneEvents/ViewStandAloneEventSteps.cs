﻿using System.Collections.Generic;
using IntegrationTests.Utilities;
using SpeedyDonkeyApi.Models;
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

            var standAloneEvent = ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEventKey);
            standAloneEvent.IsPrivate = true;
            ScenarioCache.Store(ModelKeys.StandAloneEventKey, standAloneEvent);

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

        [When(@"upcoming statnd alone events are requested")]
        public void WhenUpcomingStatndAloneEventsAreRequested()
        {
            var response = ApiCaller.Get<List<StandAloneEventModel>>(Routes.StandAloneEventsForRegistration);

            ScenarioCache.StoreResponse(response);
        }

    }
}
