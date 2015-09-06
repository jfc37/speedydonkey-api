using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/announcements")]
    public class AnnouncementApiController : GenericApiController<AnnouncementModel, Announcement>
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
            return PerformAction(model, x => new CreateAnnouncement(x));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id, [FromBody] AnnouncementModel model)
        {
            model.Id = id;
            return PerformAction(model, x => new UpdateAnnouncement(x));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new AnnouncementModel { Id = id };
            return PerformAction(model, x => new DeleteAnnouncement(x));
        }
    }
}
