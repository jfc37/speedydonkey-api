using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts.PrivateLessons;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using NodaTime;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.PrivateLessons
{
    [Binding]
    public class SetOpeningHoursSteps
    {
        [Given(@"opening hours are set")]
        public void GivenOpeningHoursAreSet()
        {
            GivenAValidOpeningHourIsReadyToBeSubmitted();
            WhenTheOpeningHourIsAttemptedToBeCreated();
            ThenTheOpeningHourCanBeRetrieved();
        }

        [Given(@"opening hours need to be changed")]
        public void GivenOpeningHoursNeedToBeChanged()
        {
            var response = ApiCaller.Get<List<TimeSlotModel>>(Routes.OpeningHours);
            var openingHours = response.Data.Single();

            var newOpeningHours = new TimeSlotModel(openingHours.Day, openingHours.OpeningTime.PlusHours(1), openingHours.ClosingTime.PlusHours(1));

            ScenarioCache.Store(ModelKeys.OpeningHoursModelKey, newOpeningHours);
        }

        [When(@"the opening hour is attempted to be updated")]
        public void WhenTheOpeningHourIsAttemptedToBeUpdated()
        {
            WhenTheOpeningHourIsAttemptedToBeCreated();
        }


        [Given(@"a valid opening hour is ready to be submitted")]
        public void GivenAValidOpeningHourIsReadyToBeSubmitted()
        {
            var openingHour = new TimeSlotModel(IsoDayOfWeek.Monday, new LocalTimeModel(10, 0), new LocalTimeModel(22, 30));

            ScenarioCache.Store(ModelKeys.OpeningHoursModelKey, openingHour);
        }

        [Given(@"a invalid opening hour is ready to be submitted")]
        public void GivenAInvalidOpeningHourIsReadyToBeSubmitted()
        {
            var openingTime = new LocalTimeModel(10, 0);
            var closingTime = openingTime.PlusHours(-1);
            var openingHour = new TimeSlotModel(IsoDayOfWeek.Monday, openingTime, closingTime);

            ScenarioCache.Store(ModelKeys.OpeningHoursModelKey, openingHour);
        }


        [When(@"the opening hour is attempted to be created")]
        public void WhenTheOpeningHourIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<TimeSlotModel>>(ScenarioCache.Get<TimeSlotModel>(ModelKeys.OpeningHoursModelKey), Routes.OpeningHours);
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the opening hour can be retrieved")]
        public void ThenTheOpeningHourCanBeRetrieved()
        {
            var response = ApiCaller.Get<List<TimeSlotModel>>(Routes.OpeningHours);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var expectedOpeningHours = ScenarioCache.Get<TimeSlotModel>(ModelKeys.OpeningHoursModelKey);
            var actualOpeningHours = response.Data.Single();

            new VerifyOpeningHoursProperties(expectedOpeningHours, actualOpeningHours)
                .Verify();
        }

        [Then(@"the opening hour cannot be retrieved")]
        public void ThenTheOpeningHourCannotBeRetrieved()
        {
            var response = ApiCaller.Get<List<TimeSlotModel>>(Routes.OpeningHours);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


    }
}
