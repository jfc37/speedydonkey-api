using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers.Announcements
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
            var createAnnouncementAction = new CreateAnnouncement(model.ToEntity());
            var result = PerformAction<CreateAnnouncement, Announcement>(createAnnouncementAction);

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<AnnouncementConfirmationModel>(new AnnouncementConfirmationModel(createAnnouncementAction.NumberOfUsersEmailed), result.ValidationResult));
        }
    }
}
