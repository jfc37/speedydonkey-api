using System;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using Contracts.Events;
using Contracts.Teachers;
using IntegrationTests.Steps.Teachers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.StandAloneEvents
{
    [Binding]
    public class CreateStandAloneEventSteps
    {
        [Given(@"a valid stand alone event is ready to be submitted")]
        public void GivenAValidStandAloneEventIsReadyToBeSubmitted()
        {
            new CommonTeacherSteps().GivenAnExistingUserIsATeacher();

            var standAloneEvent = new StandAloneEventModel
            {
                Name = "Private Lesson",
                Price = 80,
                ClassCapacity = 30,
                Teachers = new TeacherModel { Id = ScenarioCache.GetTeacherId() }.PutIntoList(),
                StartTime = DateTime.Now.AddDays(2).AddHours(1),
                EndTime = DateTime.Now.AddDays(2).AddHours(3),
            };

            ScenarioCache.Store(ModelKeys.StandAloneEvent, standAloneEvent);
        }

        [Given(@"an invalid stand alone event is ready to be submitted")]
        public void GivenAnInvalidStandAloneEventIsReadyToBeSubmitted()
        {
            new CommonTeacherSteps().GivenAnExistingUserIsATeacher();

            var standAloneEvent = new StandAloneEventModel
            {
                Teachers = new TeacherModel { Id = ScenarioCache.GetTeacherId() }.PutIntoList(),
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(3),
            };

            ScenarioCache.Store(ModelKeys.StandAloneEvent, standAloneEvent);
        }

        [When(@"the stand alone event is attempted to be created")]
        public void WhenTheStandAloneEventIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<StandAloneEventModel>>(ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEvent), Routes.StandAloneEvent);
            ScenarioCache.StoreActionResponse(response);
            ScenarioCache.Store(ModelIdKeys.StandAloneEventId, response.Data.ActionResult.Id);
        }

        [Then(@"the stand alone event can be retrieved")]
        public void ThenTheStandAloneEventCanBeRetrieved()
        {
            var response = ApiCaller.Get<StandAloneEventModel>(Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);

            Assert.Greater(response.Data.ClassCapacity, 0);
        }

    }
}
