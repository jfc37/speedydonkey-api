using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.Repositories;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [BasicAuthAuthorise]
    public class CourseGradeApiController : BaseApiController
    {
        private readonly ICourseGradeRepository _courseGradeRepository;
        private readonly IModelFactory _modelFactory;

        public CourseGradeApiController(ICourseGradeRepository courseGradeRepository, IModelFactory modelFactory)
        {
            _courseGradeRepository = courseGradeRepository;
            _modelFactory = modelFactory;
        }

        public HttpResponseMessage Get(int personId)
        {
            var allCourseGrades = _courseGradeRepository
                .GetAll(personId)
                .Select(x => _modelFactory.ToModel(Request, x))
                .ToList();
            return allCourseGrades.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, allCourseGrades)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Get(int personId, int courseId)
        {
            var courseGrade = _courseGradeRepository
                .Get(personId, courseId);
            var courseGradeModel = _modelFactory.ToModel(Request, courseGrade);
            return courseGrade != null
                ? Request.CreateResponse(HttpStatusCode.OK, courseGradeModel)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
