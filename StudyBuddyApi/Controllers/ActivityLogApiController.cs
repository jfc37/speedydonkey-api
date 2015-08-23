using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/activity_logs")]
    public class ActivityLogApiController : GenericApiController<ActivityLogModel, ActivityLog>
    {
        public ActivityLogApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<ActivityLog> repository,
            IEntitySearch<ActivityLog> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public override HttpResponseMessage Get()
        {
            return base.Get();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public override HttpResponseMessage Get(string q)
        {
            return base.Get(q);
        }
    }
}
