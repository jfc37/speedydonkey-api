using System.Collections.Generic;
using System.Net.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassAttendanceApiController : EntityPropertyApiController<ClassAttendanceModel, UserModel, Class>
    {
        public ClassAttendanceApiController(
            IRepository<Class> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, urlConstructor, cloner, actionHandlerOverlord)
        {
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Post(int id, int studentId)
        {
            var classModel = new ClassModel
            {
                Id = id,
                ActualStudents = new List<IUser>
                {
                    new UserModel {Id = studentId}
                }
            };
            return PerformAction<CheckStudentIntoClass, ClassModel, Class>(classModel, x => new CheckStudentIntoClass(x));
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Delete(int id, int studentId)
        {
            var classModel = new ClassModel
            {
                Id = id,
                ActualStudents = new List<IUser>
                {
                    new UserModel {Id = studentId}
                }
            };
            return PerformAction<RemoveStudentFromClass, ClassModel, Class>(classModel, x => new RemoveStudentFromClass(x));
        }
    }
}