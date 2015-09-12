using System.Linq;
using System.Web.Http;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using FluentNHibernate.Conventions;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/activity_logs")]
    public class ActivityLogApiController : GenericApiController<ActivityLog>
    {
        public ActivityLogApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<ActivityLog> repository,
            IEntitySearch<ActivityLog> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get()
        {
            var all = GetAll().ToList();
            if (all.IsEmpty())
            {
                return NotFound();
            }

            return Ok(all);
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get(string q)
        {
            var searchResult = Search(q).ToList();
            if (searchResult.IsEmpty())
            {
                return NotFound();
            }

            return Ok(searchResult);
        }
    }
}
