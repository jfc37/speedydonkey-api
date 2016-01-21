using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts;
using Contracts.Blocks;
using Contracts.Events;
using Contracts.Rooms;
using IntegrationTests.Utilities;
using Models;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Rooms
{
    [Binding]
    public class AssignRoomToBlockSteps
    {
        [Given(@"the block needs to be assigned a room")]
        public void GivenTheBlockNeedsToBeAssignedARoom()
        {
            ScenarioCache.Store(ModelIdKeys.BlockKeyId, 1);
        }

        [When(@"the block room assignment is requested")]
        public void WhenTheBlockRoomAssignmentIsRequested()
        {
            var blockId = ScenarioCache.GetId(ModelIdKeys.BlockKeyId);
            var roomId = ScenarioCache.GetId(ModelIdKeys.RoomKeyId);
            var response = ApiCaller.Put<ActionReponse<BlockModel>>(Routes.GetBlockRoom(blockId, roomId));
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the block details has the room")]
        public void ThenTheBlockDetailsHasTheRoom()
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId))).Data;
            var roomId = ScenarioCache.GetId(ModelIdKeys.RoomKeyId);

            Assert.IsNotNull(block.Room);
            Assert.AreEqual(roomId, block.Room.Id);
        }

        [Then(@"all the classes in the block has the room")]
        public void ThenAllTheClassesInTheBlockHasTheRoom()
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId))).Data;
            var roomId = ScenarioCache.GetId(ModelIdKeys.RoomKeyId);

            foreach (var classModel in block.Classes)
            {
                Assert.IsNotNull(classModel.Room);
                Assert.AreEqual(roomId, classModel.Room.Id);   
            }
        }

        [Then(@"the room has the classes in its schedule")]
        public void ThenTheRoomHasTheClassesInItsSchedule()
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId))).Data;
            var blockClasses = block.Classes;
            var roomScheduleResponse =
                ApiCaller.Get<List<EventModel>>(
                    Routes.GetRoomUpcomingSchedule(ScenarioCache.GetId(ModelIdKeys.RoomKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, roomScheduleResponse.StatusCode);

            Assert.Contains(blockClasses.First().Id, roomScheduleResponse.Data.Select(x => x.Id).ToList());
        }

        [Given(@"the block is assigned to the room")]
        public void GivenTheBlockIsAssignedToTheRoom()
        {
            GivenTheBlockNeedsToBeAssignedARoom();
            WhenTheBlockRoomAssignmentIsRequested();
            ThenTheBlockDetailsHasTheRoom();
            ThenAllTheClassesInTheBlockHasTheRoom();
        }

        [When(@"another block at the same time needs to be assigned to the same room")]
        public void WhenAnotherBlockAtTheSameTimeNeedsToBeAssignedToTheSameRoom()
        {
            ScenarioCache.Store(ModelIdKeys.BlockKeyId, 2);
            WhenTheBlockRoomAssignmentIsRequested();
        }

        [Then(@"all the classes in the block does not have the room")]
        public void ThenAllTheClassesInTheBlockDoesNotHaveTheRoom()
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId))).Data;

            foreach (var classModel in block.Classes)
            {
                Assert.IsNull(classModel.Room);
            }
        }

        [Then(@"the room does not have the blocks classes in its schedule")]
        public void ThenTheRoomDoesNotHaveTheBlocksClassesInItsSchedule()
        {
            var block = ApiCaller.Get<BlockModel>(Routes.GetById(Routes.Blocks, ScenarioCache.GetId(ModelIdKeys.BlockKeyId))).Data;
            var blockClasses = block.Classes;
            var roomScheduleResponse =
                ApiCaller.Get<List<EventModel>>(
                    Routes.GetRoomUpcomingSchedule(ScenarioCache.GetId(ModelIdKeys.RoomKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, roomScheduleResponse.StatusCode);

            Assert.IsEmpty(roomScheduleResponse.Data.Where(x => x.Id == blockClasses.First().Id));
        }

        [Given(@"the pending block is to be held in the room")]
        public void GivenThePendingBlockIsToBeHeldInTheRoom()
        {
            var block = ScenarioCache.Get<BlockModel>(ModelKeys.BlockModelKey);
            var roomId = ScenarioCache.GetId(ModelIdKeys.RoomKeyId);

            block.Room = new RoomModel(roomId);

            ScenarioCache.Store(ModelKeys.BlockModelKey, block);
        }
    }
}
