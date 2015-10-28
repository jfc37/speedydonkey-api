using System.Net;
using ActionHandlers;
using IntegrationTests.Steps.Passes;
using IntegrationTests.Steps.PassTemplates;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Classes
{
    [Binding]
    public class DeleteClassSteps
    {
        [Given(@"a class needs to be deleted")]
        public void GivenAClassNeedsToBeDeleted()
        {
            ScenarioCache.Store(ModelIdKeys.ClassKeyId, 1);
        }

        [Given(@"a user has attended the class")]
        public void GivenAUserHasAttendedTheClass()
        {
            new CommonPassSteps().GivenTheUserHasAValidClipPass();
            new ClassAttendanceSteps().GivenTheTeacherHasCheckedTheStudentIn();
        }

        [When(@"the class is deleted")]
        public void WhenTheClassIsDeleted()
        {
            var response = ApiCaller.Delete<ActionReponse<ClassModel>>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassKeyId)));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the class can not be retrieved")]
        public void ThenTheClassCanNotBeRetrieved()
        {
            var response = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassKeyId)));

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Then(@"the class can be retrieved")]
        public void ThenTheClassCanBeRetrieved()
        {
            var response = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
