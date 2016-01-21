using System.Net;
using ActionHandlers;
using Contracts.Rooms;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Rooms
{
    [Binding]
    public class UpdateRoomSteps
    {
        [Given(@"a room exists")]
        public void GivenARoomExists()
        {
            var createRoomSteps = new CreateRoomSteps();
            createRoomSteps.GivenAValidRoomIsReadyToBeSubmitted();
            createRoomSteps.WhenTheRoomIsAttemptedToBeCreated();
            createRoomSteps.ThenTheRoomCanBeRetrieved();
        }

        [Given(@"the room needs to be changed")]
        public void GivenTheRoomNeedsToBeChanged()
        {
            var room = ScenarioCache.Get<RoomModel>(ModelKeys.RoomModelKey);
            room.Name = "Changed Name";
            room.Location = "Changed Location";

            ScenarioCache.Store(ModelKeys.RoomModelKey, room);
        }

        [When(@"the room is updated")]
        public void WhenTheRoomIsUpdated()
        {
            var response = ApiCaller.Put<ActionReponse<RoomModel>>(ScenarioCache.Get<RoomModel>(ModelKeys.RoomModelKey), Routes.GetById(Routes.Room, ScenarioCache.GetId(ModelIdKeys.RoomKeyId)));
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the request is successful")]
        public void ThenTheRequestIsSuccessful()
        {
            var httpStatusCode = ScenarioCache.GetResponseStatus();
            Assert.Contains(httpStatusCode, new []{HttpStatusCode.OK, HttpStatusCode.Created});
        }

        [Given(@"the room has invalid details")]
        public void GivenTheRoomHasInvalidDetails()
        {
            var room = ScenarioCache.Get<RoomModel>(ModelKeys.RoomModelKey);
            room.Name = "";
            room.Location = null;

            ScenarioCache.Store(ModelKeys.RoomModelKey, room);
        }

        [Then(@"the request is unsuccessful")]
        public void ThenTheRequestIsUnsuccessful()
        {
            var httpStatusCode = ScenarioCache.GetResponseStatus();
            Assert.AreEqual(HttpStatusCode.BadRequest, httpStatusCode);
        }

        [Then(@"the request returns not found")]
        public void ThenTheRequestReturnsNotFound()
        {
            var httpStatusCode = ScenarioCache.GetResponseStatus();
            Assert.AreEqual(HttpStatusCode.NotFound, httpStatusCode);
        }


    }
}
