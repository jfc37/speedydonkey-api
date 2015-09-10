using System;
using System.Linq;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Steps.Teachers;
using IntegrationTests.Utilities;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Levels
{
    [Binding]
    public class CreateLevelSteps
    {
        private const string LevelModelKey = "levelModel";

        [Given(@"a valid level is ready to be submitted")]
        public void GivenAValidLevelIsReadyToBeSubmitted()
        {
            new CommonTeacherSteps().GivenAnExistingUserIsATeacher();

            var level = new LevelModel
            {
                ClassMinutes = 60,
                ClassesInBlock = 6,
                EndTime = DateTime.Now.AddYears(1),
                Name = "Charleston Level 1",
                StartTime = DateTime.Now.AddMonths(1),
                Teachers = new TeacherModel {Id = ScenarioCache.GetTeacherId()}.PutIntoList<ITeacher>()
            };

            ScenarioCache.Store(LevelModelKey, level);
        }

        [When(@"the level is attempted to be created")]
        public void WhenTheLevelIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<LevelModel>>(ScenarioCache.Get<LevelModel>(LevelModelKey), Routes.Levels);
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"level can be retrieved")]
        public void ThenLevelCanBeRetrieved()
        {
            var response = ApiCaller.Get<LevelModel>(Routes.GetLevelById(ScenarioCache.GetActionResponse<LevelModel>().Id));
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Given(@"an invalid level is ready to be submitted")]
        public void GivenAnInvalidLevelIsReadyToBeSubmitted()
        {
            var level = new LevelModel
            {
                ClassMinutes = 60,
                EndTime = DateTime.Now.AddYears(1),
                Name = "Charleston Level 1",
                StartTime = DateTime.Now.AddMonths(1),
            };

            ScenarioCache.Store(LevelModelKey, level);
        }

        [Then(@"level can not be retrieved")]
        public void ThenLevelCanNotBeRetrieved()
        {
            var response = ApiCaller.Get<LevelModel>(Routes.Levels);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
