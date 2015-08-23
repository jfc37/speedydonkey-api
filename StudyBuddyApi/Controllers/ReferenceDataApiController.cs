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
    [RoutePrefix("api/reference")]
    public class ReferenceDataApiController : GenericApiController<ReferenceDataModel, ReferenceData>
    {
        public ReferenceDataApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<ReferenceData> repository, 
            IEntitySearch<ReferenceData> entitySearch) : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Post([FromBody]ReferenceDataModel model)
        {
            return PerformAction(model, x => new CreateReferenceData(x));
        }
    }
}