using System;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Steps.Teachers;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Levels
{
    [Binding]
    public class CreateLevelSteps
    {
        [Given(@"a valid level is ready to be submitted")]
        public void GivenAValidLevelIsReadyToBeSubmitted()
        {
            new CommonTeacherSteps().GivenAnExistingUserIsATeacher();

            var level = new LevelModel
            {
                ClassMinutes = 60,
                ClassesInBlock = 6,
                EndTime = new DateTime(2016, 10, 1, 11, 0, 0),
                Name = "Charleston Level 1",
                StartTime = new DateTime(2015, 10, 1, 11, 0, 0),
                Teachers = new TeacherModel {Id = ScenarioCache.GetTeacherId()}.PutIntoList()
            };

            ScenarioCache.Store(ModelKeys.LevelModelKey, level);
        }

        [When(@"the level is attempted to be created")]
        public void WhenTheLevelIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<LevelModel>>(ScenarioCache.Get<LevelModel>(ModelKeys.LevelModelKey), Routes.Levels);
            ScenarioCache.StoreActionResponse(response);
            ScenarioCache.StoreLevelId(response.Data.ActionResult.Id);
        }

        [Then(@"level can be retrieved")]
        public void ThenLevelCanBeRetrieved()
        {
            var response = ApiCaller.Get<LevelModel>(Routes.GetLevelById(ScenarioCache.GetActionResponse<LevelModel>().Id));
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            new VerifyLevelProperties(ScenarioCache.Get<LevelModel>(ModelKeys.LevelModelKey), response.Data)
                .Verify();
        }

        [Given(@"an invalid level is ready to be submitted")]
        public void GivenAnInvalidLevelIsReadyToBeSubmitted()
        {
            var level = new LevelModel
            {
                ClassMinutes = 60,
                EndTime = new DateTime(2016, 10, 1, 11, 0, 0),
                Name = "Charleston Level 1",
                StartTime = new DateTime(2015, 10, 1, 11, 0, 0),
            };

            ScenarioCache.Store(ModelKeys.LevelModelKey, level);
        }

        [Then(@"level can not be retrieved")]
        public void ThenLevelCanNotBeRetrieved()
        {
            var response = ApiCaller.Get<LevelModel>(Routes.Levels);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
