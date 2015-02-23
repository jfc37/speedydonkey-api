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
    public class LectureApiController : BaseApiController
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IModelFactory _modelFactory;
        private readonly IEntitySearch<Lecture> _entitySearch;

        public LectureApiController(ILectureRepository lectureRepository, 
            IActionHandlerOverlord actionHandlerOverlord,
            IModelFactory modelFactory,
            IEntitySearch<Lecture> entitySearch)
        {
            _lectureRepository = lectureRepository;
            _actionHandlerOverlord = actionHandlerOverlord;
            _modelFactory = modelFactory;
            _entitySearch = entitySearch;
        }

        // GET: api/LectureApi
        public HttpResponseMessage Get(int courseId)
        {
            var allLectures = _lectureRepository
                .GetAll(courseId)
                .Select(x => _modelFactory.ToModel(Request, x))
                .ToList();
            return allLectures.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, allLectures)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [BasicAuthAuthorise]
        public HttpResponseMessage Get(string q)
        {
            var matchingCourses = _entitySearch.Search(q);
            if (!matchingCourses.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK,
                matchingCourses.Select(x => _modelFactory.ToModel(Request, x)).ToList());
        }

        // GET: api/LectureApi/5
        public HttpResponseMessage Get(int courseId, int lectureId)
        {
            var lecture = _modelFactory.ToModel(Request, _lectureRepository.Get(lectureId));
            return lecture != null
                ? Request.CreateResponse(HttpStatusCode.OK, lecture)
                : Request.CreateResponse(HttpStatusCode.NotFound);

        }

        // POST: api/LectureApi
        public HttpResponseMessage Post(int courseId, [FromBody]LectureModel lectureModel)
        {
            if (lectureModel != null)
            {
                lectureModel.Course = new CourseModel{Id = courseId};
            }
            var lecture = _modelFactory.Parse(lectureModel);
            var updateLecture = new CreateLecture(lecture);
            ActionReponse<Lecture> result = _actionHandlerOverlord.HandleAction<CreateLecture, Lecture>(updateLecture);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode,
                new ActionReponse<LectureModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Put(int courseId, int lectureId, [FromBody]LectureModel lectureModel)
        {
            lectureModel.Id = lectureId;
            var lecture = _modelFactory.Parse(lectureModel);

            var updateLecture = new UpdateLecture(lecture);
            ActionReponse<Lecture> result = _actionHandlerOverlord.HandleAction<UpdateLecture, Lecture>(updateLecture);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<LectureModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Delete(int courseId, int lectureId)
        {
            var lecture = _lectureRepository.Get(lectureId);
            if (lecture == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var deleteLecture = new DeleteLecture(lecture);
            var actionResult = _actionHandlerOverlord.HandleAction<DeleteLecture, Lecture>(deleteLecture);

            HttpStatusCode responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
