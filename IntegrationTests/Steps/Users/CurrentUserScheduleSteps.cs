using System.Collections.Generic;
using System.Net;
using Contracts.Events;
using IntegrationTests.Steps.Blocks;
using IntegrationTests.Utilities;
using NUnit.Framework;
using RestSharp;
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
            ScenarioCache.Store(ModelIdKeys.User, 1);
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


    [Binding]
    public class UserScheduleSteps
    {
        private const string ScheduleResponseKey = "scheduleResponse";

        [When(@"the user schedule is retrieved")]
        public void WhenTheUserScheduleIsRetrieved()
        {
            var response = ApiCaller.Get<List<EventModel>>(Routes.GetUserSchedule(ScenarioCache.GetUserId()));
            ScenarioCache.Store(ScheduleResponseKey, response);
        }

        [Then(@"the user's schedule is not emtpy")]
        public void ThenTheUserSScheduleIsNotEmtpy()
        {
            var response = ScenarioCache.Get<RestResponse<List<EventModel>>>(ScheduleResponseKey);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);
        }

        [Then(@"the user's schedule is emtpy")]
        public void ThenTheUserSScheduleIsEmtpy()
        {
            var response = ScenarioCache.Get<RestResponse<List<EventModel>>>(ScheduleResponseKey);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
