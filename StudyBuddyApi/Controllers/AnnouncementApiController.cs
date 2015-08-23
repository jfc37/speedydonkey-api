using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using Newtonsoft.Json;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class AnnouncementApiController : GenericApiController<AnnouncementModel, Announcement>
    {
        public AnnouncementApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<Announcement> repository,
            IEntitySearch<Announcement> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post([FromBody] AnnouncementModel model)
        {
            return PerformAction(model, x => new CreateAnnouncement(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id, [FromBody] AnnouncementModel model)
        {
            model.Id = id;
            return PerformAction(model, x => new UpdateAnnouncement(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new AnnouncementModel { Id = id };
            return PerformAction(model, x => new DeleteAnnouncement(x));
        }
    }
}
