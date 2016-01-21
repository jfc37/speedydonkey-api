using System.Net;
using Contracts.Teachers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Teachers
{
    [Binding]
    public class CreateTeacherSteps
    {

        [Then(@"user is added to the list of teachers")]
        public void ThenUserIsAddedToTheListOfTeachers()
        {
            var allTeachersResponse = ApiCaller.Get<TeacherModel>(Routes.GetTeacherById(ScenarioCache.GetTeacherId()));

            Assert.AreEqual(HttpStatusCode.OK, allTeachersResponse.StatusCode);
        }

        [Then(@"user is still on the list of teachers")]
        public void ThenUserIsStillOnTheListOfTeachers()
        {
            ThenUserIsAddedToTheListOfTeachers();
        }
    }
}
