using ActionHandlers;
using Contracts.Teachers;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Teachers
{
    [Binding]
    public class SetRatesForATeacherSteps
    {
        [When(@"the rate for the teacher is changed")]
        public void WhenTheRateForTheTeacherIsChanged()
        {
            var rate = new TeacherRateModel(100, 200);
            ScenarioCache.Store(ModelKeys.TeacherRate, rate);

            var teacherId = ScenarioCache.GetId(ModelIdKeys.TeacherId);
            var url = Routes.GetTeacherRatesById(teacherId);

            var response = ApiCaller.Post<ActionReponse<TeacherModel>>(rate, url);
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the rate for the teacher is updated")]
        public void ThenTheRateForTheTeacherIsUpdated()
        {
            var teacherId = ScenarioCache.GetId(ModelIdKeys.TeacherId);
            var url = Routes.GetTeacherRatesById(teacherId);

            var response = ApiCaller.Get<TeacherRateModel>(url);

            var actualRate = response.Data;
            var expectedRate = ScenarioCache.Get<TeacherRateModel>(ModelKeys.TeacherRate);

            new VerifyTeacherRateProperties(expectedRate, actualRate).Verify();
        }
    }
}