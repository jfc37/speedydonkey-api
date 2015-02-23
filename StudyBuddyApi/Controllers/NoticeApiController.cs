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
    public class NoticeApiController : BaseApiController
    {
        private readonly INoticeRepository _noticeRepository;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IModelFactory _modelFactory;

        public NoticeApiController(INoticeRepository noticeRepository, IActionHandlerOverlord actionHandlerOverlord, IModelFactory modelFactory)
        {
            _noticeRepository = noticeRepository;
            _actionHandlerOverlord = actionHandlerOverlord;
            _modelFactory = modelFactory;
        }

        // GET: api/NoticeApi
        public HttpResponseMessage Get(int courseId)
        {
            var allNotices = _noticeRepository
                .GetAll(courseId)
                .Select(x => _modelFactory.ToModel(Request, x))
                .ToList();
            return allNotices.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, allNotices)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // GET: api/NoticeApi/5
        public HttpResponseMessage Get(int courseId, int noticeId)
        {
            var notice = _modelFactory.ToModel(Request, _noticeRepository.Get(noticeId));
            return notice != null
                ? Request.CreateResponse(HttpStatusCode.OK, notice)
                : Request.CreateResponse(HttpStatusCode.NotFound);

        }

        // POST: api/NoticeApi
        public HttpResponseMessage Post(int courseId, [FromBody]NoticeModel noticeModel)
        {
            if (noticeModel != null)
            {
                noticeModel.Course = new CourseModel{Id = courseId};
            }
            var notice = _modelFactory.Parse(noticeModel);
            var createNotice = new CreateNotice(notice);
            ActionReponse<Notice> result = _actionHandlerOverlord.HandleAction<CreateNotice, Notice>(createNotice);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode,
                new ActionReponse<NoticeModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Put(int courseId, int noticeId, [FromBody]NoticeModel noticeModel)
        {
            noticeModel.Id = noticeId;
            var notice = _modelFactory.Parse(noticeModel);

            var updateNotice = new UpdateNotice(notice);
            ActionReponse<Notice> result = _actionHandlerOverlord.HandleAction<UpdateNotice, Notice>(updateNotice);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<NoticeModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Delete(int courseId, int noticeId)
        {
            var notice = _noticeRepository.Get(noticeId);
            if (notice == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var deleteNotice = new DeleteNotice(notice);
            var actionResult = _actionHandlerOverlord.HandleAction<DeleteNotice, Notice>(deleteNotice);

            HttpStatusCode responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
