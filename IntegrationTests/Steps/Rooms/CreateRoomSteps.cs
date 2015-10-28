using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Rooms
{
    [Binding]
    public class CreateRoomSteps
    {
        [Given(@"a valid room is ready to be submitted")]
        public void GivenAValidRoomIsReadyToBeSubmitted()
        {
            var room = new RoomModel("Room One", "Cuba Street");
            ScenarioCache.Store(ModelKeys.RoomModelKey, room);
        }

        [Given(@"an invalid room is ready to be submitted")]
        public void GivenAnInvalidRoomIsReadyToBeSubmitted()
        {
            var room = new RoomModel();
            ScenarioCache.Store(ModelKeys.RoomModelKey, room);
        }

        [When(@"the room is attempted to be created")]
        public void WhenTheRoomIsAttemptedToBeCreated()
        {
            var response = ApiCaller.Post<ActionReponse<RoomModel>>(ScenarioCache.Get<RoomModel>(ModelKeys.RoomModelKey), Routes.Room);
            ScenarioCache.StoreActionResponse(response);
            ScenarioCache.Store(ModelIdKeys.RoomKeyId, response.Data.ActionResult.Id);
        }

        [Then(@"the room can be retrieved")]
        public void ThenTheRoomCanBeRetrieved()
        {
            var roomId = ScenarioCache.GetId(ModelIdKeys.RoomKeyId);
            Assert.Greater(roomId, 0);

            var response = ApiCaller.Get<RoomModel>(Routes.GetById(Routes.Room, roomId));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            new VerifyRoomProperties(ScenarioCache.Get<RoomModel>(ModelKeys.RoomModelKey), response.Data)
                .Verify();
        }

        [Then(@"the room can not be retrieved")]
        public void ThenTheRoomCanNotBeRetrieved()
        {
            var response = ApiCaller.Get<RoomModel>(Routes.Room);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
