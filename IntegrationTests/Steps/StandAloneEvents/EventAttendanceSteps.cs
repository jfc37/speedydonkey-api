using System.Linq;
using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.StandAloneEvents
{
    [Binding]
    public class EventAttendanceSteps
    {
        [Given(@"the teacher has checked the student into the event")]
        public void GivenTheTeacherHasCheckedTheStudentIntoTheEvent()
        {
            WhenTheTeacherChecksTheStudentIntoTheEvent();
        }


        [When(@"the teacher checks the student into the event")]
        public void WhenTheTeacherChecksTheStudentIntoTheEvent()
        {
            var response =
                ApiCaller.Post<ActionReponse<StandAloneEventModel>>(
                    Routes.GetAttendEvent(ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId),
                        ScenarioCache.GetId(ModelIdKeys.UserIdKey)));

            ScenarioCache.StoreActionResponse(response);
        }

        [When(@"the teacher unchecks the student into the event")]
        public void WhenTheTeacherUnchecksTheStudentIntoTheEvent()
        {
            var response =
                ApiCaller.Delete<ActionReponse<StandAloneEventModel>>(
                    Routes.GetAttendEvent(ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId),
                        ScenarioCache.GetId(ModelIdKeys.UserIdKey)));

            ScenarioCache.StoreActionResponse(response);
        }


        [Then(@"the student is marked against the event")]
        public void ThenTheStudentIsMarkedAgainstTheEvent()
        {
            var response = ApiCaller.Get<StandAloneEventModel>(Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(ScenarioCache.GetId(ModelIdKeys.UserIdKey), response.Data.ActualStudents.Select(x => x.Id).ToList());
        }

        [Then(@"the student isnt marked against the event")]
        public void ThenTheStudentIsntMarkedAgainstTheEvent()
        {
            var response = ApiCaller.Get<StandAloneEventModel>(Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsEmpty(response.Data.ActualStudents);
        }

    }
}
