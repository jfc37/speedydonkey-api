using ActionHandlers;
using IntegrationTests.Utilities;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Rooms
{
    [Binding]
    public class DeleteRoomSteps
    {
        [When(@"the room is deleted")]
        public void WhenTheRoomIsDeleted()
        {
            var response = ApiCaller.Delete<ActionReponse<RoomModel>>(Routes.GetById(Routes.Room, ScenarioCache.GetId(ModelIdKeys.RoomKeyId)));
            ScenarioCache.StoreActionResponse(response);
        }

    }
}
