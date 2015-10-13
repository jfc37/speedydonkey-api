using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
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
    }
}
