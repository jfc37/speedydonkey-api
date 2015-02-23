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
    public class AssignmentApiController : BaseApiController
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IModelFactory _modelFactory;
        private readonly IEntitySearch<Assignment> _entitySearch;

        public AssignmentApiController(IAssignmentRepository assignmentRepository, IActionHandlerOverlord actionHandlerOverlord, IModelFactory modelFactory, IEntitySearch<Assignment> entitySearch)
        {
            _assignmentRepository = assignmentRepository;
            _actionHandlerOverlord = actionHandlerOverlord;
            _modelFactory = modelFactory;
            _entitySearch = entitySearch;
        }

        // GET: api/AssignmentApi
        public HttpResponseMessage Get(int courseId)
        {
            var allAssignments = _assignmentRepository
                .GetAll(courseId)
                .Select(x => _modelFactory.ToModel(Request, x))
                .ToList();
            return allAssignments.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, allAssignments)
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

        // GET: api/AssignmentApi/5
        public HttpResponseMessage Get(int courseId, int courseWorkId)
        {
            var assignment = _modelFactory.ToModel(Request, _assignmentRepository.Get(courseWorkId));
            return assignment != null
                ? Request.CreateResponse(HttpStatusCode.OK, assignment)
                : Request.CreateResponse(HttpStatusCode.NotFound);

        }

        // POST: api/AssignmentApi
        public HttpResponseMessage Post(int courseId, [FromBody]AssignmentModel assignmentModel)
        {
            if (assignmentModel != null)
            {
                assignmentModel.Course = new CourseModel{Id = courseId};
            }
            var assignment = _modelFactory.Parse(assignmentModel);
            var createAssignment = new CreateAssignment(assignment);
            ActionReponse<Assignment> result = _actionHandlerOverlord.HandleAction<CreateAssignment, Assignment>(createAssignment);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode,
                new ActionReponse<AssignmentModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Put(int courseId, int courseWorkId, [FromBody]AssignmentModel assignmentModel)
        {
            assignmentModel.Id = courseWorkId;
            var assignment = _modelFactory.Parse(assignmentModel);

            var updateAssignment = new UpdateAssignment(assignment);
            ActionReponse<Assignment> result = _actionHandlerOverlord.HandleAction<UpdateAssignment, Assignment>(updateAssignment);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<AssignmentModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Delete(int courseId, int courseWorkId)
        {
            var assignment = _assignmentRepository.Get(courseWorkId);
            if (assignment == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var deleteAssignment = new DeleteCourseWork(assignment);
            var actionResult = _actionHandlerOverlord.HandleAction<DeleteCourseWork, CourseWork>(deleteAssignment);

            HttpStatusCode responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
