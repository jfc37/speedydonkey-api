using System.Net;
using ActionHandlers;
using Contracts.Teachers;
using IntegrationTests.Steps.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Teachers
{
    [Binding]
    public class CommonTeacherSteps
    {
        [Given(@"an existing user is a teacher")]
        public void GivenAnExistingUserIsATeacher()
        {
            new CommonUserSteps().GivenAUserExists();
            WhenUserIsSetUpAsATeacher();
        }

        [Given(@"the current user is a teacher")]
        public void GivenTheCurrentUserIsATeacher()
        {
            ScenarioCache.Store(ModelIdKeys.UserId, 1);
            WhenUserIsSetUpAsATeacher();
        }
        
        [When(@"user is set up as a teacher")]
        public void WhenUserIsSetUpAsATeacher()
        {
            WhenUserIsAttemptedToBeSetUpAsATeacher();

            Assert.AreEqual(HttpStatusCode.Created, ScenarioCache.GetResponseStatus());

            var teacher = ScenarioCache.GetActionResponse<TeacherModel>();
            ScenarioCache.Store(ModelIdKeys.TeacherId, teacher.Id);
        }

        [When(@"user is attempted to be set up as a teacher")]
        public void WhenUserIsAttemptedToBeSetUpAsATeacher()
        {
            var userId = ScenarioCache.GetUserId();
            var response = ApiCaller.Post<ActionReponse<TeacherModel>>(Routes.GetTeacherById(userId));

            ScenarioCache.StoreActionResponse(response);
        }
    }
}