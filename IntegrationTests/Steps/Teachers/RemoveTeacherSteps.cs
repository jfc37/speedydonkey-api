using System.Linq;
using System.Net;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelFunctions;
using Models.QueryExtensions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Teachers
{
    [Binding]
    public class RemoveTeacherSteps
    {
        [When(@"teacher is removed")]
        public void WhenTeacherIsRemoved()
        {
            var response = ApiCaller.Delete<bool>(Routes.GetTeacherById(ScenarioCache.GetTeacherId()));

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Then(@"user is removed from the list of teachers")]
        public void ThenUserIsRemovedFromTheListOfTeachers()
        {
            var allTeachers = TeacherFunction.GetAllTeachers();

            Assert.IsEmpty(allTeachers);
        }

        [Then(@"user still exists")]
        public void ThenUserStillExists()
        {
            var allUsers = UserFunction.GetAllUsers();

            var matchingUser = allUsers.SingleWithId(ScenarioCache.GetUserId());

            Assert.IsNotNull(matchingUser);
        }

    }
}
