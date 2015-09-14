using System.Web.Http;
using Action;
using ActionHandlers;
using Common.Extensions;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/classes")]
    public class ClassAttendanceApiController : EntityPropertyApiController
    {
        public ClassAttendanceApiController(IActionHandlerOverlord actionHandlerOverlord)
            : base(actionHandlerOverlord)
        {
        }

        [Route("{id}/attendance/{studentId}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Post(int id, int studentId)
        {
            var classModel = new Class(id)
            {
                Id = id,
                ActualStudents = new User(studentId).PutIntoList()
            };
            var result = PerformAction<CheckStudentIntoClass, Class>(new CheckStudentIntoClass(classModel));

            return new ActionResultToOkHttpActionResult<Class, ClassModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id}/attendance/{studentId}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Delete(int id, int studentId)
        {
            var classModel = new Class(id)
            {
                ActualStudents = new User(studentId).PutIntoList()
            };
            var result = PerformAction<RemoveStudentFromClass, Class>(new RemoveStudentFromClass(classModel));

            return new ActionResultToOkHttpActionResult<Class, ClassModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}