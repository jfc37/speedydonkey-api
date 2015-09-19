using System.Collections.Generic;
using IntegrationTests.Utilities;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Teachers
{
    [Binding]
    public class RetrieveTeacherSteps
    {
        [When(@"all teachers are retreived")]
        public void WhenAllTeachersAreRetreived()
        {
            var response = ApiCaller.Get<TeacherModel>(Routes.Teachers);
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }

        [When(@"a teacher search is performed")]
        public void WhenATeacherSearchIsPerformed()
        {
            var response = ApiCaller.Get<List<TeacherModel>>(Routes.GetTeacherSearch("id_=_1"));
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }

        [When(@"a teacher is retrieved by id")]
        public void WhenATeacherIsRetrievedById()
        {
            var response = ApiCaller.Get<TeacherModel>(Routes.GetTeacherById(ScenarioCache.GetActionResponse<TeacherModel>().Id));
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }

    }
}
