using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassAttendanceApiController : EntityPropertyApiController
    {
        public ClassAttendanceApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(actionHandlerOverlord)
        {
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Post(int id, int studentId)
        {
            var classModel = new Class
            {
                Id = id,
                ActualStudents = new List<User>
                {
                    new User {Id = studentId}
                }
            };
            var result = PerformAction<CheckStudentIntoClass, Class>(new CheckStudentIntoClass(classModel));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<ClassModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Delete(int id, int studentId)
        {
            var classModel = new Class
            {
                Id = id,
                ActualStudents = new List<User>
                {
                    new User {Id = studentId}
                }
            };
            var result = PerformAction<RemoveStudentFromClass, Class>(new RemoveStudentFromClass(classModel));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<ClassModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
    }
}