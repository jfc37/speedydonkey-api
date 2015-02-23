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
    public class ExamApiController : BaseApiController
    {
        private readonly IExamRepository _examRepository;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IModelFactory _modelFactory;
        private readonly IEntitySearch<Exam> _entitySearch;

        public ExamApiController(IExamRepository examRepository, IActionHandlerOverlord actionHandlerOverlord, IModelFactory modelFactory, IEntitySearch<Exam> entitySearch)
        {
            _examRepository = examRepository;
            _actionHandlerOverlord = actionHandlerOverlord;
            _modelFactory = modelFactory;
            _entitySearch = entitySearch;
        }

        // GET: api/ExamApi
        public HttpResponseMessage Get(int courseId)
        {
            var allExams = _examRepository
                .GetAll(courseId)
                .Select(x => _modelFactory.ToModel(Request, x))
                .ToList();
            return allExams.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, allExams)
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

        // GET: api/ExamApi/5
        public HttpResponseMessage Get(int courseId, int courseWorkId)
        {
            var exam = _modelFactory.ToModel(Request, _examRepository.Get(courseWorkId));
            return exam != null
                ? Request.CreateResponse(HttpStatusCode.OK, exam)
                : Request.CreateResponse(HttpStatusCode.NotFound);

        }

        // POST: api/ExamApi
        public HttpResponseMessage Post(int courseId, [FromBody]ExamModel examModel)
        {
            if (examModel != null)
            {
                examModel.Course = new CourseModel{Id = courseId};
            }
            var exam = _modelFactory.Parse(examModel);
            var createExam = new CreateExam(exam);
            ActionReponse<Exam> result = _actionHandlerOverlord.HandleAction<CreateExam, Exam>(createExam);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode,
                new ActionReponse<ExamModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Put(int courseId, int courseWorkId, [FromBody]ExamModel examModel)
        {
                examModel.Id = courseWorkId;
                var exam = _modelFactory.Parse(examModel);

                var updateExam = new UpdateExam(exam);
                ActionReponse<Exam> result = _actionHandlerOverlord.HandleAction<UpdateExam, Exam>(updateExam);
                HttpStatusCode responseCode = result.ValidationResult.IsValid
                    ? HttpStatusCode.OK
                    : HttpStatusCode.BadRequest;
                return Request.CreateResponse(
                    responseCode,
                    new ActionReponse<ExamModel>
                    {
                        ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                        ValidationResult = result.ValidationResult
                    });
        }

        public HttpResponseMessage Delete(int courseId, int courseWorkId)
        {
            var assignment = _examRepository.Get(courseWorkId);
            if (assignment == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var deleteExam = new DeleteCourseWork(assignment);
            var actionResult = _actionHandlerOverlord.HandleAction<DeleteCourseWork, CourseWork>(deleteExam);

            HttpStatusCode responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
