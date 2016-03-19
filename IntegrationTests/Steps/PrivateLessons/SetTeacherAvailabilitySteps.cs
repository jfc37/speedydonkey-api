using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using Contracts.PrivateLessons;
using IntegrationTests.Utilities;
using NodaTime;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.PrivateLessons
{
    [Binding]
    public class SetTeacherAvailabilitySteps
    {
        [Given(@"a valid teacher availability is ready to be submitted")]
        public void GivenAValidTeacherAvailabilityIsReadyToBeSubmitted()
        {
            var availability = new TimeSlotModel(IsoDayOfWeek.Monday, new LocalTimeModel(10, 0), new LocalTimeModel(22, 30));
            var teacherAvailability = new TeacherAvailabilityModel(availability.PutIntoList());

            ScenarioCache.Store(ModelKeys.TeacherAvailability, teacherAvailability);
        }

        [When(@"the teacher availability is attempted to be created")]
        public void WhenTheTeacherAvailabilityIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<TeacherAvailabilityModel>>(ScenarioCache.Get<TeacherAvailabilityModel>(ModelKeys.TeacherAvailability), Routes.GetCurrentTeacherAvailabilities());
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the teacher availability can be retrieved")]
        public void ThenTheTeacherAvailabilityCanBeRetrieved()
        {
            var response = ApiCaller.Get<List<TeacherAvailabilityModel>>(Routes.TeacherAvailability);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
            var actualTeacherAvailability = response.Data.SingleOrDefault();

            Assert.IsNotNull(actualTeacherAvailability);


        }

    }
}
