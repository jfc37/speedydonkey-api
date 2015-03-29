using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class ReferenceDataApiController : GenericApiController<ReferenceDataModel, ReferenceData>
    {
        public ReferenceDataApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor, 
            IRepository<ReferenceData> repository, 
            ICommonInterfaceCloner cloner, 
            IEntitySearch<ReferenceData> entitySearch) : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
        }

        [ClaimsAuthorise(Claim = Claim.CreateReferenceData)]
        public HttpResponseMessage Post([FromBody]ReferenceDataModel model)
        {
            return PerformAction(model, x => new CreateReferenceData(x));
        }
    }
}