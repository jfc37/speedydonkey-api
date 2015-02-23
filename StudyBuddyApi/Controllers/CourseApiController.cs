using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
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
    public class CourseApiController : BaseApiController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IModelFactory _modelFactory;
        private readonly IEntitySearch<Course> _courseEntitySearch;

        public CourseApiController(ICourseRepository courseRepository, IActionHandlerOverlord actionHandlerOverlord, IModelFactory modelFactory, IEntitySearch<Course> courseEntitySearch)
        {
            _courseRepository = courseRepository;
            _actionHandlerOverlord = actionHandlerOverlord;
            _modelFactory = modelFactory;
            _courseEntitySearch = courseEntitySearch;
        }

        // GET: api/CourseApi
        public HttpResponseMessage Get()
        {
            var allCourses = _courseRepository
                .GetAll()
                .Select(x => _modelFactory.ToModel(Request, x))
                .ToList();
            return allCourses.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, allCourses)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [BasicAuthAuthorise]
        public HttpResponseMessage Get(string q)
        {
            var matchingCourses = _courseEntitySearch.Search(q);
            if (!matchingCourses.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK,
                matchingCourses.Select(x => _modelFactory.ToModel(Request, x)).ToList());
        }

        // GET: api/CourseApi/5
        public HttpResponseMessage Get(int courseId)
        {
            var course = _modelFactory.ToModel(Request, _courseRepository.Get(courseId));
            return course != null
                ? Request.CreateResponse(HttpStatusCode.OK, course)
                : Request.CreateResponse(HttpStatusCode.NotFound);

        }

        // POST: api/CourseApi
        public HttpResponseMessage Post(int personId, [FromBody]CourseModel courseModel)
        {
            if (courseModel != null)
            {
                courseModel.Professors.Add(new ProfessorModel { Id = personId });
            }
            var course = _modelFactory.Parse(courseModel);
            var createCourse = new CreateCourse(course);
            ActionReponse<Course> result = _actionHandlerOverlord.HandleAction<CreateCourse, Course>(createCourse);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode,
                new ActionReponse<CourseModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Put(int courseId, [FromBody]CourseModel courseModel)
        {
            courseModel.Id = courseId;
            var course = _modelFactory.Parse(courseModel);

            var updateCourse = new UpdateCourse(course);
            ActionReponse<Course> result = _actionHandlerOverlord.HandleAction<UpdateCourse, Course>(updateCourse);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<CourseModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Delete(int courseId)
        {
            var course = _courseRepository.Get(courseId);
            if (course == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var deleteCourse = new DeleteCourse(course);
            var actionResult = _actionHandlerOverlord.HandleAction<DeleteCourse, Course>(deleteCourse);

            HttpStatusCode responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
