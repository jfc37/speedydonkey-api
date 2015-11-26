using System;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Steps.Teachers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
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
                Teachers = new TeacherModel { Id = ScenarioCache.GetTeacherId() }.PutIntoList(),
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(3),
            };

            ScenarioCache.Store(ModelKeys.StandAloneEventKey, standAloneEvent);
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

            ScenarioCache.Store(ModelKeys.StandAloneEventKey, standAloneEvent);
        }

        [When(@"the stand alone event is attempted to be created")]
        public void WhenTheStandAloneEventIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<StandAloneEventModel>>(ScenarioCache.Get<StandAloneEventModel>(ModelKeys.StandAloneEventKey), Routes.StandAloneEvent);
            ScenarioCache.StoreActionResponse(response);
            ScenarioCache.Store(ModelIdKeys.StandAloneEventKeyId, response.Data.ActionResult.Id);
        }

        [Then(@"the stand alone event can be retrieved")]
        public void ThenTheStandAloneEventCanBeRetrieved()
        {
            var response = ApiCaller.Get<StandAloneEventModel>(Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
        }

    }
}
