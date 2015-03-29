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
    public class LevelApiController : GenericApiController<LevelModel, Level>
    {
        public LevelApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor,
            IRepository<Level> repository,
            ICommonInterfaceCloner cloner,
            IEntitySearch<Level> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
        }

        [ClaimsAuthorise(Claim = Claim.CreateLevel)]
        public HttpResponseMessage Post([FromBody] LevelModel model)
        {
            return PerformAction(model, x => new CreateLevel(x));
        }
    }
}
