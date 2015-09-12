using System.Collections.Generic;
using System.Net;
using IntegrationTests.Steps.Blocks;
using IntegrationTests.Utilities;
using NUnit.Framework;
using RestSharp;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class CurrentUserScheduleSteps
    {
        private const string ScheduleResponseKey = "scheduleResponse";

        [Given(@"the current user enrols in the block")]
        public void GivenTheCurrentUserEnrolsInTheBlock()
        {
            ScenarioCache.StoreUserId(1);
            new CommonBlockSteps().GivenTheUserEnrolsInTheBlock();
        }

        [Given(@"the current user isn't enrolled in any blocks")]
        public void GivenTheCurrentUserIsnTEnrolledInAnyBlocks()
        {
        }

        [When(@"the current user schedule is retrieved")]
        public void WhenTheCurrentUserScheduleIsRetrieved()
        {
            var response = ApiCaller.Get<List<EventModel>>(Routes.CurrentUserSchedule);
            ScenarioCache.Store(ScheduleResponseKey, response);
        }

        [Then(@"the current user's schedule is not emtpy")]
        public void ThenTheCurrentUserSScheduleIsNotEmtpy()
        {
            var response = ScenarioCache.Get<RestResponse<List<EventModel>>>(ScheduleResponseKey);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);
        }

        [Then(@"the current user's schedule is emtpy")]
        public void ThenTheCurrentUserSScheduleIsEmtpy()
        {
            var response = ScenarioCache.Get<RestResponse<List<EventModel>>>(ScheduleResponseKey);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
