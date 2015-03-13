using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
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

        public HttpResponseMessage Post([FromBody]ReferenceDataModel model)
        {
            return Post(model, x => new CreateReferenceData(x));
        }
    }
}