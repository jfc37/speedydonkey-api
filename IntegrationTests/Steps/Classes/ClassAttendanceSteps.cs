using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts;
using Contracts.Classes;
using Contracts.Passes;
using Contracts.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Classes
{
    [Binding]
    public class ClassAttendanceSteps
    {
        [Given(@"the user attends a class")]
        public void GivenTheUserAttendsAClass()
        {
            ScenarioCache.Store(ModelIdKeys.ClassId, 1);
        }

        [Given(@"the teacher has checked the student in")]
        public void GivenTheTeacherHasCheckedTheStudentIn()
        {
            GivenTheUserAttendsAClass();
            WhenTheTeacherChecksTheStudentIn();
            ThenCheckInIsSuccessful();
            ThenTheStudentIsMarkedAgainstClass();
            ThenAClipHasBeenRemovedFromThePass();
        }

        [When(@"the teacher checks the student in")]
        public void WhenTheTeacherChecksTheStudentIn()
        {
            var response =
                ApiCaller.Post<ActionReponse<ClassModel>>(
                    Routes.GetAttendClass(ScenarioCache.GetId(ModelIdKeys.ClassId),
                        ScenarioCache.GetId(ModelIdKeys.UserId)));

            ScenarioCache.StoreActionResponse(response);
        }

        [When(@"the teacher unchecks the student in")]
        public void WhenTheTeacherUnchecksTheStudentIn()
        {
            var response =
                ApiCaller.Delete<ActionReponse<ClassModel>>(
                    Routes.GetAttendClass(ScenarioCache.GetId(ModelIdKeys.ClassId),
                        ScenarioCache.GetId(ModelIdKeys.UserId)));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"check in is successful")]
        public void ThenCheckInIsSuccessful()
        {
            Assert.AreEqual(HttpStatusCode.OK, ScenarioCache.GetResponseStatus());;
        }


        [Then(@"the student is marked against class")]
        public void ThenTheStudentIsMarkedAgainstClass()
        {
            var response = ApiCaller.Get<List<UserModel>>(Routes.GetClassAttendance(ScenarioCache.GetId(ModelIdKeys.ClassId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(ScenarioCache.GetId(ModelIdKeys.UserId), response.Data.Select(x => x.Id).ToList());
        }

        [Then(@"a clip has been removed from the pass")]
        public void ThenAClipHasBeenRemovedFromThePass()
        {
            var passTemplate = ScenarioCache.Get<PassTemplateModel>(ModelKeys.PassTemplate);
            var expectedClipsRemaining = passTemplate.ClassesValidFor - 1;

            var response = ApiCaller.Get<List<ClipPassModel>>(Routes.GetUserPasses(ScenarioCache.GetId(ModelIdKeys.UserId)));
            var actualClipsRemaining = response.Data.Single().ClipsRemaining;

            Assert.AreEqual(expectedClipsRemaining, actualClipsRemaining);
        }

        [Then(@"the student isnt marked against class")]
        public void ThenTheStudentIsntMarkedAgainstClass()
        {
            var response = ApiCaller.Get<List<UserModel>>(Routes.GetClassAttendance(ScenarioCache.GetId(ModelIdKeys.ClassId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsEmpty(response.Data);
        }

        [Then(@"a clip has not been removed from the pass")]
        public void ThenAClipHasNotBeenRemovedFromThePass()
        {
            var passTemplate = ScenarioCache.Get<PassTemplateModel>(ModelKeys.PassTemplate);
            var expectedClipsRemaining = passTemplate.ClassesValidFor;

            var response = ApiCaller.Get<List<ClipPassModel>>(Routes.GetUserPasses(ScenarioCache.GetId(ModelIdKeys.UserId)));
            var actualClipsRemaining = response.Data.Single().ClipsRemaining;

            Assert.AreEqual(expectedClipsRemaining, actualClipsRemaining);
        }

    }
}
