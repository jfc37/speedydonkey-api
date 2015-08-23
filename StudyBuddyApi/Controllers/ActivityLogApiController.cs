using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class ActivityLogApiController : GenericApiController<ActivityLogModel, ActivityLog>
    {
        public ActivityLogApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<ActivityLog> repository,
            IEntitySearch<ActivityLog> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }


        [ClaimsAuthorise(Claim = Claim.Admin)]
        public override HttpResponseMessage Get()
        {
            return base.Get();
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public override HttpResponseMessage Get(string q)
        {
            return base.Get(q);
        }
    }
}
