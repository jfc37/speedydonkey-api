using System.Collections.Generic;
using ActionHandlers;
using Common.Extensions;
using Contracts.Announcements;
using Contracts.Blocks;
using IntegrationTests.Utilities;
using NUnit.Framework;
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

            ScenarioCache.Store(ModelKeys.Announcement, announcement);
        }

        [Given(@"the announcement is to be sent to the block")]
        public void GivenTheAnnouncementIsToBeSentToTheBlock()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.Announcement);
            announcement.NotifyAll = false;
            announcement.Receivers = new BlockModel(1).PutIntoList();

            ScenarioCache.Store(ModelKeys.Announcement, announcement);
        }

        [Given(@"the announcement is to be sent to multiple blocks")]
        public void GivenTheAnnouncementIsToBeSentToMultipleBlocks()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.Announcement);
            announcement.NotifyAll = false;
            announcement.Receivers = new List<BlockModel>
            {
                new BlockModel(1),
                new BlockModel(2)
            };

            ScenarioCache.Store(ModelKeys.Announcement, announcement);
        }

        [Given(@"the announcement is to be sent to all users")]
        public void GivenTheAnnouncementIsToBeSentToAllUsers()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.Announcement);
            announcement.NotifyAll = true;

            ScenarioCache.Store(ModelKeys.Announcement, announcement);
        }

        [Given(@"the announcement is to be sent to no one")]
        public void GivenTheAnnouncementIsToBeSentToNoOne()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.Announcement);
            announcement.NotifyAll = false;
            announcement.Receivers = new List<BlockModel>();

            ScenarioCache.Store(ModelKeys.Announcement, announcement);
        }

        [Given(@"the announcement is missing the message")]
        public void GivenTheAnnouncementIsMissingTheMessage()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.Announcement);
            announcement.Message = null;

            ScenarioCache.Store(ModelKeys.Announcement, announcement);
        }

        [Given(@"the announcement is missing the subject")]
        public void GivenTheAnnouncementIsMissingTheSubject()
        {
            var announcement = ScenarioCache.Get<AnnouncementModel>(ModelKeys.Announcement);
            announcement.Subject = null;

            ScenarioCache.Store(ModelKeys.Announcement, announcement);
        }

        [When(@"the announcement is attempted to be created")]
        public void WhenTheAnnouncementIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<AnnouncementConfirmationModel>>(ScenarioCache.Get<AnnouncementModel>(ModelKeys.Announcement), Routes.Announcements);
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"an email was sent to '(.*)' users")]
        public void ThenAnEmailWasSentToUsers(int numberOfUsersEmailed)
        {
            var announcementConfirmation = ScenarioCache.GetActionResponse<AnnouncementConfirmationModel>();

            Assert.AreEqual(numberOfUsersEmailed, announcementConfirmation.NumberOfUsersEmailed);
        }
    }
}
