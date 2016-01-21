using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts;
using Contracts.Classes;
using Contracts.Events;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Rooms
{
    [Binding]
    public class AssignRoomToClassSteps
    {
        [Given(@"a class needs to be assigned a room")]
        public void GivenAClassNeedsToBeAssignedARoom()
        {
            ScenarioCache.Store(ModelIdKeys.ClassKeyId, 1);
        }

        [Given(@"a class is assigned to the room")]
        public void GivenAClassIsAssignedToTheRoom()
        {
            GivenAClassNeedsToBeAssignedARoom();
            WhenTheClassRoomAssignmentIsRequested();
            ThenTheClassDetailsHasTheRoom();
            ThenTheRoomHasTheClassInItsSchedule();
        }

        [When(@"the class room assignment is requested")]
        public void WhenTheClassRoomAssignmentIsRequested()
        {
            var classId = ScenarioCache.GetId(ModelIdKeys.ClassKeyId);
            var roomId = ScenarioCache.GetId(ModelIdKeys.RoomKeyId);
            var response = ApiCaller.Put<ActionReponse<ClassModel>>(Routes.GetClassRoom(classId, roomId));
            ScenarioCache.StoreActionResponse(response);
        }

        [When(@"another class at the same time needs to be assigned to the same room")]
        public void WhenAnotherClassAtTheSameTimeNeedsToBeAssignedToTheSameRoom()
        {
            ScenarioCache.Store(ModelIdKeys.ClassKeyId, 7);
            WhenTheClassRoomAssignmentIsRequested();
        }

        [Then(@"the class details has the room")]
        public void ThenTheClassDetailsHasTheRoom()
        {
            var theClass = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassKeyId))).Data;
            var roomId = ScenarioCache.GetId(ModelIdKeys.RoomKeyId);

            Assert.IsNotNull(theClass.Room);
            Assert.AreEqual(roomId, theClass.Room.Id);
        }

        [Then(@"the class details does not have the room")]
        public void ThenTheClassDetailsDoesNotHaveTheRoom()
        {
            var theClass = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassKeyId))).Data;

            Assert.IsNull(theClass.Room);
        }

        [Then(@"the room has the class in its schedule")]
        public void ThenTheRoomHasTheClassInItsSchedule()
        {
            var classId = ScenarioCache.GetId(ModelIdKeys.ClassKeyId);
            var roomScheduleResponse =
                ApiCaller.Get<List<EventModel>>(
                    Routes.GetRoomUpcomingSchedule(ScenarioCache.GetId(ModelIdKeys.RoomKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, roomScheduleResponse.StatusCode);

            Assert.Contains(classId, roomScheduleResponse.Data.Select(x => x.Id).ToList());
        }

        [Then(@"the room does not have the class in its schedule")]
        public void ThenTheRoomDoesNotHaveTheClassInItsSchedule()
        {
            var classId = ScenarioCache.GetId(ModelIdKeys.ClassKeyId);
            var roomScheduleResponse = ApiCaller.Get<List<EventModel>>(Routes.GetRoomUpcomingSchedule(ScenarioCache.GetId(ModelIdKeys.RoomKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, roomScheduleResponse.StatusCode);

            var matchingClass = roomScheduleResponse.Data.SingleOrDefault(x => x.Id == classId);
            Assert.IsNull(matchingClass);
        }

    }
}
