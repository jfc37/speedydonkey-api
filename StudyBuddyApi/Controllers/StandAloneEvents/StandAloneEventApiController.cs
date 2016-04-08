using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Action.StandAloneEvents;
using ActionHandlers;
using Common;
using Common.Extensions;
using Contracts.Events;
using Contracts.MappingExtensions;
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
        private readonly ICurrentUser _currentUser;

        public StandAloneEventApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<StandAloneEvent> repository,
            IEntitySearch<StandAloneEvent> entitySearch,
            ICurrentUser currentUser)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
            _currentUser = currentUser;
        }

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
            return new SetToHttpActionResult<EventForRegistrationModel>(this, new AvailableEventsForRegistrationFilter().Filter(GetAll(), _currentUser.Id), x => x)
                .Do();
        }
    }
    /// <summary>
    /// Filters down to only events available for the public to register for
    /// </summary>
    /// <seealso cref="StandAloneEvent" />
    public class AvailableEventsForRegistrationFilter
    {
        /// <summary>
        /// Filters down to only events available for the public to register for
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="userId"></param>
        /// <returns>Events available for registration</returns>
        public IEnumerable<EventForRegistrationModel> Filter(IEnumerable<StandAloneEvent> query, int userId)
        {
            var today = DateTime.Today;
            var blah =  query
                .Where(x => !x.IsPrivate)
                .Where(x => x.StartTime.Date.IsOnOrAfter(today))
                .Select(x => x.ToEventForRegistrationModel(userId));

            return blah;
        }
    }
}