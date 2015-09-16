using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Data.CodeChunks;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/announcements")]
    public class AnnouncementApiController : GenericApiController<Announcement>
    {
        public AnnouncementApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<Announcement> repository,
            IEntitySearch<Announcement> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post([FromBody] AnnouncementModel model)
        {
            var result = PerformAction<CreateAnnouncement, Announcement>(new CreateAnnouncement(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<AnnouncementModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id, [FromBody] AnnouncementModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdateAnnouncement, Announcement>(new UpdateAnnouncement(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<AnnouncementModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new AnnouncementModel { Id = id };
            var result = PerformAction<DeleteAnnouncement, Announcement>(new DeleteAnnouncement(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<AnnouncementModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
    }
}
