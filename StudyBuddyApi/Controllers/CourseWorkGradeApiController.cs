using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [BasicAuthAuthorise]
    public class CourseWorkGradeApiController : BaseApiController
    {
        private readonly ICourseWorkGradeRepository _courseWorkGradeRepository;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IModelFactory _modelFactory;

        public CourseWorkGradeApiController(ICourseWorkGradeRepository courseWorkGradeRepository, IActionHandlerOverlord actionHandlerOverlord, IModelFactory modelFactory)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
            _actionHandlerOverlord = actionHandlerOverlord;
            _modelFactory = modelFactory;
        }

        public HttpResponseMessage Get(int personId, int courseId)
        {
            var allCourseWorkGrades = _courseWorkGradeRepository
                .GetAll(personId, courseId)
                .Select(x => _modelFactory.ToModel(Request, x))
                .ToList();
            return allCourseWorkGrades.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, allCourseWorkGrades)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Get(int personId, int courseId, int courseWorkId)
        {
            var courseWorkGrade = _courseWorkGradeRepository
                .Get(personId, courseId, courseWorkId);
            var courseWorkGradeModel = _modelFactory.ToModel(Request, courseWorkGrade);
            return courseWorkGrade != null
                ? Request.CreateResponse(HttpStatusCode.OK, courseWorkGradeModel)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Post(int personId, int courseId, int courseWorkId, CourseWorkGradeModel courseWorkGradeModel)
        {
            try
            {
                courseWorkGradeModel.CourseWork = new CourseWorkModel{Id = courseWorkId};
                courseWorkGradeModel.CourseGrade = new CourseGradeModel
                {
                    Student = new StudentModel { Id = personId },
                    Course = new CourseModel {Id = courseId}
                };

                var courseWorkGrade = _modelFactory.Parse(courseWorkGradeModel);
                var addCourseWorkGrade = new AddCourseWorkGrade(courseWorkGrade);

                ActionReponse<CourseWorkGrade> result = _actionHandlerOverlord.HandleAction<AddCourseWorkGrade, CourseWorkGrade>(addCourseWorkGrade);
                HttpStatusCode responseCode = result.ValidationResult.IsValid
                    ? HttpStatusCode.OK
                    : HttpStatusCode.BadRequest;

                return Request.CreateResponse(responseCode,
                    new ActionReponse<CourseWorkGradeModel>
                    {
                        ValidationResult = result.ValidationResult,
                        ActionResult = _modelFactory.ToModel(Request, result.ActionResult)
                    });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Put(int personId, int courseId, int courseWorkId, [FromBody]CourseWorkGradeModel courseWorkGradeModel)
        {
            try
            {
                courseWorkGradeModel.Id = courseWorkId;
                courseWorkGradeModel.CourseWork = new CourseWorkModel {Id = courseWorkId};
                courseWorkGradeModel.CourseGrade = new CourseGradeModel
                {
                    Course = new CourseModel {Id = courseId},
                    Student = new StudentModel {Id = personId}
                };
                var courseWorkGrade = _modelFactory.Parse(courseWorkGradeModel);

                var updateCourseWorkGrade = new UpdateCourseWorkGrade(courseWorkGrade);
                ActionReponse<CourseWorkGrade> result = _actionHandlerOverlord.HandleAction<UpdateCourseWorkGrade, CourseWorkGrade>(updateCourseWorkGrade);
                HttpStatusCode responseCode = result.ValidationResult.IsValid
                    ? HttpStatusCode.OK
                    : HttpStatusCode.BadRequest;
                return Request.CreateResponse(
                    responseCode,
                    new ActionReponse<CourseWorkGradeModel>
                    {
                        ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                        ValidationResult = result.ValidationResult
                    });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Delete(int personId, int courseId, int courseWorkId)
        {
            var courseWorkGrade = _courseWorkGradeRepository.Get(personId, courseId, courseWorkId);
            if (courseWorkGrade == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var deleteCourseWorkGrade = new DeleteCourseWorkGrade(courseWorkGrade);
            var actionResult = _actionHandlerOverlord.HandleAction<DeleteCourseWorkGrade, CourseWorkGrade>(deleteCourseWorkGrade);

            HttpStatusCode responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
