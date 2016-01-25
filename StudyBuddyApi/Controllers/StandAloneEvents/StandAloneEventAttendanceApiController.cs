using System.Web.Http;
using Action.StandAloneEvents;
using ActionHandlers;
using Common.Extensions;
using Contracts;
using Contracts.Events;
using Contracts.MappingExtensions;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.StandAloneEvents
{
    [RoutePrefix("api/stand-alone-events")]
    public class StandAloneEventAttendanceApiController : EntityPropertyApiController
    {
        public StandAloneEventAttendanceApiController(
            IActionHandlerOverlord actionHandlerOverlord)
            : base(actionHandlerOverlord)
        {
        }

        [Route("{id:int}/attendance/{studentId:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Post(int id, int studentId)
        {
            var classModel = new StandAloneEvent(id)
            {
                Id = id,
                ActualStudents = new User(studentId).PutIntoList()
            };
            var result = PerformAction<CheckStudentIntoEvent, Event>(new CheckStudentIntoEvent(classModel));

            return new ActionResultToOkHttpActionResult<Event, EventModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}/attendance/{studentId:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Delete(int id, int studentId)
        {
            var classModel = new StandAloneEvent(id)
            {
                ActualStudents = new User(studentId).PutIntoList()
            };
            var result = PerformAction<RemoveStudentFromEvent, Event>(new RemoveStudentFromEvent(classModel));

            return new ActionResultToOkHttpActionResult<Event, EventModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}