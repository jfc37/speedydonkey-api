using System.Web.Http;
using Action.Classes;
using ActionHandlers;
using Common.Extensions;
using Contracts;
using Contracts.Classes;
using Contracts.Events;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Classes
{
    [RoutePrefix("api/classes")]
    public class ClassAttendanceApiController : EntityPropertyApiController
    {
        private readonly IRepository<Class> _repository;

        public ClassAttendanceApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<Class> repository)
            : base(actionHandlerOverlord)
        {
            _repository = repository;
        }

        [Route("{id:int}/attendance")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            var entity = _repository.Get(id);
            return entity.IsNotNull()
                ? (IHttpActionResult)Ok(new ClassAttendanceModel().ConvertFromEntity(entity))
                : NotFound();
        }

        [Route("{id:int}/attendance/{studentId:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Post(int id, int studentId)
        {
            var classModel = new Class(id)
            {
                Id = id,
                ActualStudents = new User(studentId).PutIntoList()
            };
            var result = PerformAction<CheckStudentIntoClass, Class>(new CheckStudentIntoClass(classModel));

            return new ActionResultToOkHttpActionResult<Class, EventModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}/attendance/{studentId:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Delete(int id, int studentId)
        {
            var classModel = new Class(id)
            {
                ActualStudents = new User(studentId).PutIntoList()
            };
            var result = PerformAction<RemoveStudentFromClass, Class>(new RemoveStudentFromClass(classModel));

            return new ActionResultToOkHttpActionResult<Class, EventModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}