using System.Linq;
using System.Web.Http;
using Action.Rooms;
using ActionHandlers;
using Common.Extensions;
using Contracts;
using Contracts.MappingExtensions;
using Contracts.Rooms;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Rooms
{
    [RoutePrefix("api/rooms")]
    public class RoomsApiController : GenericApiController<Room>
    {
        public RoomsApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<Room> repository,
            IEntitySearch<Room> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<Room>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<Room>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<Room>(this, GetById(id), x => x.ToModel()).Do();
        }

        [Route("{id:int}/upcoming-schedule")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult GetUpcomingSchedule(int id)
        {
            var room = GetById(id);

            return room.IsNull()
                ? (IHttpActionResult) NotFound()
                : Ok(room.GetUpcomingSchedule().Select(x => x.ToModel()));
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post([FromBody]RoomModel model)
        {
            var result = PerformAction<CreateRoom, Room>(new CreateRoom(model.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<Room, RoomModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Put(int id, [FromBody]RoomModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdateRoom, Room>(new UpdateRoom(model.ToEntity()));

            return new ActionResultToOkHttpActionResult<Room, RoomModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Delete(int id)
        {
            var model = new Room(id);
            var result = PerformAction<DeleteRoom, Room>(new DeleteRoom(model));

            return new ActionResultToCreatedHttpActionResult<Room, RoomModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}