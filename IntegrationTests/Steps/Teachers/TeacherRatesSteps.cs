using System.Collections.Generic;
using ActionHandlers;
using Contracts.Teachers;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Teachers
{
    [Binding]
    public class TeacherRatesSteps
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

        [When(@"the rates for all teachers are requested")]
        public void WhenTheRatesForAllTeachersAreRequested()
        {
            var url = Routes.TeacherRates;

            var response = ApiCaller.Get<List<TeacherRateModel>>(url);

            ScenarioCache.StoreResponse(response);
            ScenarioCache.Store(ModelKeys.TeacherRate, response.Data);
        }

        [Then(@"the rates for '(.*)' teachers are retrieved")]
        public void ThenTheRatesForTeachersAreRetrieved(int expectedNumberOfRates)
        {
            var rates = ScenarioCache.Get<List<TeacherRateModel>>(ModelKeys.TeacherRate);

            Assert.AreEqual(expectedNumberOfRates, rates.Count);
        }


    }
}