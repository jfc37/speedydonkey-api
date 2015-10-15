using System.Collections.Generic;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Announcements
{
    [Binding]
    public class CreateAnnoucementSteps
    {
        [Given(@"a valid announcement is ready to be submitted")]
        public void GivenAValidAnnouncementIsReadyToBeSubmitted()
        {
            var announcement = new AnnouncementModel
            {
                Message = "Hello",
                Subject = "Test",
                NotifyAll = true
            };

            ScenarioCache.Store(ModelKeys.AnnouncementKey, announcement);
        }

        [Given(@"the announcement is to be sent to the block")]
        public void GivenTheAnnouncementIsToBeSentToTheBlock()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.AnnouncementKey);
            announcement.NotifyAll = false;
            announcement.Receivers = new BlockModel(1).PutIntoList();

            ScenarioCache.Store(ModelKeys.AnnouncementKey, announcement);
        }

        [Given(@"the announcement is to be sent to multiple blocks")]
        public void GivenTheAnnouncementIsToBeSentToMultipleBlocks()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.AnnouncementKey);
            announcement.NotifyAll = false;
            announcement.Receivers = new List<BlockModel>
            {
                new BlockModel(1),
                new BlockModel(2)
            };

            ScenarioCache.Store(ModelKeys.AnnouncementKey, announcement);
        }

        [Given(@"the announcement is to be sent to all users")]
        public void GivenTheAnnouncementIsToBeSentToAllUsers()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.AnnouncementKey);
            announcement.NotifyAll = true;

            ScenarioCache.Store(ModelKeys.AnnouncementKey, announcement);
        }

        [Given(@"the announcement is to be sent to no one")]
        public void GivenTheAnnouncementIsToBeSentToNoOne()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.AnnouncementKey);
            announcement.NotifyAll = false;
            announcement.Receivers = new List<BlockModel>();

            ScenarioCache.Store(ModelKeys.AnnouncementKey, announcement);
        }

        [Given(@"the announcement is missing the message")]
        public void GivenTheAnnouncementIsMissingTheMessage()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.AnnouncementKey);
            announcement.Message = null;

            ScenarioCache.Store(ModelKeys.AnnouncementKey, announcement);
        }

        [Given(@"the announcement is missing the subject")]
        public void GivenTheAnnouncementIsMissingTheSubject()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.AnnouncementKey);
            announcement.Subject = null;

            ScenarioCache.Store(ModelKeys.AnnouncementKey, announcement);
        }

        [When(@"the announcement is attempted to be created")]
        public void WhenTheAnnouncementIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<AnnouncementModel>>(ScenarioCache.Get<AnnouncementModel>(ModelKeys.AnnouncementKey), Routes.Announcements);
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"announcement request is successful")]
        public void ThenAnnouncementRequestIsSuccessful()
        {
            var httpStatusCode = ScenarioCache.GetResponseStatus();
            Assert.AreEqual(HttpStatusCode.Created, httpStatusCode);
        }

        [Then(@"announcement request fails")]
        public void ThenAnnouncementRequestFails()
        {
            var httpStatusCode = ScenarioCache.GetResponseStatus();
            Assert.AreEqual(HttpStatusCode.BadRequest, httpStatusCode);
        }
    }
}
