using System.Web.Http;
using Action.StandAloneEvents;
using ActionHandlers;
using Contracts;
using Contracts.Events;
using Contracts.MappingExtensions;
using Data.QueryFilters;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.StandAloneEvents
{
    [RoutePrefix("api/stand-alone-events")]
    public class StandAloneEventApiController : GenericApiController<StandAloneEvent>
    {
        public StandAloneEventApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<StandAloneEvent> repository,
            IEntitySearch<StandAloneEvent> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        [NullModelActionFilter]
        public IHttpActionResult Post([FromBody] StandAloneEventModel standAloneEventModel)
        {
            var result = PerformAction<CreateStandAloneEvent, StandAloneEvent>(new CreateStandAloneEvent(standAloneEventModel.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<StandAloneEvent, EventModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Put(int id, [FromBody] StandAloneEventModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdateStandAloneEvent, StandAloneEvent>(new UpdateStandAloneEvent(model.ToEntity()));

            return new ActionResultToOkHttpActionResult<StandAloneEvent, EventModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<StandAloneEvent>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<StandAloneEvent>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<StandAloneEvent>(this, GetById(id), x => x.ToModel()).Do();
        }

        [Route("for-registration")]
        public IHttpActionResult GetForEnrolment()
        {
            return new SetToHttpActionResult<StandAloneEvent>(this, new AvailableEventsForRegistrationFilter().Filter(GetAll()), x => x.ToStripedModel()).Do();
        }
    }
}