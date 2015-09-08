using System.Collections.Generic;
using System.Net;
using ActionHandlers;
using IntegrationTests.Steps.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Teachers
{
    [Binding]
    public class CreateTeacherSteps
    {
        [When(@"user is attempted to be set up as a teacher")]
        public void WhenUserIsAttemptedToBeSetUpAsATeacher()
        {
            var userId = ScenarioCache.GetUserId();
            var response = ApiCaller.Post<ActionReponse<UserModel>>(Routes.GetTeacherById(userId));

            ScenarioCache.StoreResponse(response);
        }

        [When(@"user is set up as a teacher")]
        public void WhenUserIsSetUpAsATeacher()
        {
            WhenUserIsAttemptedToBeSetUpAsATeacher();

            var response = ScenarioCache.GetResponse<ActionReponse<UserModel>>();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            ScenarioCache.StoreResponse(response);
        }

        [Then(@"user is added to the list of teachers")]
        public void ThenUserIsAddedToTheListOfTeachers()
        {
            var allTeachersResponse = ApiCaller.Get<List<TeacherModel>>(Routes.Teachers);

            Assert.IsNotEmpty(allTeachersResponse.Data);
        }

        [Given(@"an existing user is a teacher")]
        public void GivenAnExistingUserIsATeacher()
        {
            new CommonUserSteps().GivenAUserExists();
            WhenUserIsSetUpAsATeacher();
        }

        [Then(@"user is still on the list of teachers")]
        public void ThenUserIsStillOnTheListOfTeachers()
        {
            ThenUserIsAddedToTheListOfTeachers();
        }
    }
}
