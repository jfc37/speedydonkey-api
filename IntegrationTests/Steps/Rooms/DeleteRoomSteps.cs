using ActionHandlers;
using Contracts.Rooms;
using IntegrationTests.Utilities;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Rooms
{
    [Binding]
    public class DeleteRoomSteps
    {
        [When(@"the room is deleted")]
        public void WhenTheRoomIsDeleted()
        {
            var response = ApiCaller.Delete<ActionReponse<RoomModel>>(Routes.GetById(Routes.Room, ScenarioCache.GetId(ModelIdKeys.RoomId)));
            ScenarioCache.StoreActionResponse(response);
        }

    }
}
