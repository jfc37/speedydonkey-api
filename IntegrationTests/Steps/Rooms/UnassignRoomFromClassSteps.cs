using ActionHandlers;
using IntegrationTests.Utilities;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Rooms
{
    [Binding]
    public class UnassignRoomFromClassSteps
    {
        [When(@"the class room unassignment is requested")]
        public void WhenTheClassRoomUnassignmentIsRequested()
        {
            var classId = ScenarioCache.GetId(ModelIdKeys.ClassKeyId);
            var response = ApiCaller.Delete<ActionReponse<ClassModel>>(Routes.GetClassRoom(classId));
            ScenarioCache.StoreActionResponse(response);
        }
    }
}
