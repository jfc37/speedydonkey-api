using System.Collections.Generic;
using System.Linq;
using Contracts.Events;
using IntegrationTests.Utilities;
using NUnit.Framework;
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

        [Given(@"a stand alone event with '(.*)' teacher rate exists")]
        public void GivenAStandAloneEventWithTeacherRateExists(decimal teacherRate)
        {
            var createSteps = new CreateStandAloneEventSteps();
            createSteps.GivenAValidStandAloneEventIsReadyToBeSubmitted();

            var standAloneEvent = ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEvent);
            standAloneEvent.TeacherRate = teacherRate;
            ScenarioCache.Store(ModelKeys.StandAloneEvent, standAloneEvent);

            createSteps.WhenTheStandAloneEventIsAttemptedToBeCreated();
            createSteps.ThenTheStandAloneEventCanBeRetrieved();
        }


        [When(@"upcoming stand alone events are requested")]
        public void WhenUpcomingStatndAloneEventsAreRequested()
        {
            var response = ApiCaller.Get<List<EventForRegistrationModel>>(Routes.StandAloneEventsForRegistration);

            ScenarioCache.StoreResponse(response);
        }

        [Given(@"the user is registered for a stand alone event")]
        public void GivenTheUserIsRegisteredForAStandAloneEvent()
        {
            GivenAStandAloneEventExists();
            var standAloneEventRegistrationSteps = new StandAloneEventRegistrationSteps();
            ScenarioCache.Store(ModelIdKeys.UserId, 1);
            standAloneEventRegistrationSteps.WhenTheUserRegistersForTheStandAloneEvent();
            standAloneEventRegistrationSteps.ThenTheUserIsRegisteredInTheStandAloneEvent();
        }

        [Then(@"the student sees the stand alone event")]
        public void ThenTheStudentSeesTheStandAloneEvent()
        {
            var events = ScenarioCache.GetResponse<List<EventForRegistrationModel>>();

            Assert.IsNotEmpty(events);
            Assert.AreEqual(1, events.Count);

            events.ForEach(x => Assert.Greater(x.SpacesAvailable, 0));
        }

        [Then(@"the student is not marked as already attending")]
        public void ThenTheStudentIsNotMarkedAsAlreadyAttending()
        {
            var events = ScenarioCache.GetResponse<List<EventForRegistrationModel>>();

            Assert.IsFalse(events.Single().IsAlreadyRegistered);
        }

        [Then(@"the student is marked as already attending")]
        public void ThenTheStudentIsMarkedAsAlreadyAttending()
        {
            var events = ScenarioCache.GetResponse<List<EventForRegistrationModel>>();

            Assert.IsTrue(events.Single().IsAlreadyRegistered);
        }

    }
}
