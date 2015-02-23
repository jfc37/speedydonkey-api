using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [BasicAuthAuthorise]
    public class StudentApiController : PersonApiController<Student, PersonModel>
    {
        public StudentApiController(IPersonRepository<Student> personRepository, 
            IActionHandlerOverlord actionHandlerOverlord, 
            IModelFactory modelFactory,
            IEntitySearch<Student> entitySearch) : base(personRepository, actionHandlerOverlord, modelFactory, entitySearch)
        {
        }

        public HttpResponseMessage Post(int userId, StudentModel studentModel)
        {
            return base.Post(userId, studentModel);
        }

        /// <summary>
        /// Enrolling a student into a course
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(int personId, int courseId)
        {
            //Add course to student's enrolled courses
            var enrolStudent = new EnrolStudent(new Student { Id = personId, EnroledCourses = new[] { new Course { Id = courseId } } });
            ActionReponse<Student> result = ActionHandlerOverlord.HandleAction<EnrolStudent, Student>(enrolStudent);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode,
                new ActionReponse<StudentModel>
                {
                    ValidationResult = result.ValidationResult,
                    ActionResult = ModelFactory.ToModel(Request, result.ActionResult)
                });
        }

        public HttpResponseMessage Put(int personId, [FromBody]StudentModel studentModel)
        {
            studentModel.Id = personId;
            var student = ModelFactory.Parse(studentModel);

            var updateStudent = new UpdateStudent(student);
            ActionReponse<Student> result = ActionHandlerOverlord.HandleAction<UpdateStudent, Student>(updateStudent);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<StudentModel>
                {
                    ActionResult = ModelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Delete(int personId, int courseId)
        {
            var student = PersonRepository.Get(personId);
            if (student == null || student.EnroledCourses.All(x => x.Id != courseId))
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var unenrolStudent = new UnenrolStudent(new Student{ Id = personId, EnroledCourses = new []{new Course{ Id = courseId}}});
            var actionResult = ActionHandlerOverlord.HandleAction<UnenrolStudent, Student>(unenrolStudent);

            HttpStatusCode responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
