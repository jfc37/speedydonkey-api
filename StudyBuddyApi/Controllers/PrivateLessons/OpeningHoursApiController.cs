using System.Web.Http;
using Action.PrivateLessons;
using ActionHandlers;
using Common;
using Contracts.MappingExtensions;
using Contracts.PrivateLessons;
using Contracts.Teachers;
using Data.Repositories;
using Data.Searches;
using Models;
using Models.PrivateLessons;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.PrivateLessons
{
    [RoutePrefix("api/opening-hours")]
    public class OpeningHoursApiController : GenericApiController<TimeSlot>
    {
        public OpeningHoursApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<TimeSlot> repository,
            IEntitySearch<TimeSlot> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<TimeSlot>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post([FromBody] TimeSlotModel model)
        {
            var result = PerformAction<SetOpeningHours, TimeSlot>(new SetOpeningHours(model.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<TimeSlot, TimeSlotModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }

    [RoutePrefix("api/teacher-availabilities")]
    public class TeacherAvailabilitiesApiController : GenericApiController<TeacherAvailability>
    {
        private readonly ICurrentUser _currentUser;

        public TeacherAvailabilitiesApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<TeacherAvailability> repository, 
            IEntitySearch<TeacherAvailability> entitySearch,
            ICurrentUser currentUser) 
            : base(actionHandlerOverlord, repository, entitySearch)
        {
            _currentUser = currentUser;
        }

        [Route]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<TeacherAvailability>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route("current")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Post([FromBody]TeacherAvailabilityModel model)
        {
            model.Teacher = new TeacherModel(_currentUser.Id);

            var result = PerformAction<SetTeacherAvailability, TeacherAvailability>(new SetTeacherAvailability(model.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<TeacherAvailability, TeacherAvailabilityModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}
