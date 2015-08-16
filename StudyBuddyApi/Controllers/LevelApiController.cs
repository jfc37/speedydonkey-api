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

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post([FromBody] LevelModel model)
        {
            return PerformAction(model, x => new CreateLevel(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id,  [FromBody] LevelModel model)
        {
            model.Id = id;
            return PerformAction(model, x => new UpdateLevel(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new LevelModel{ Id = id};
            return PerformAction(model, x => new DeleteLevel(x));
        }
    }
}
