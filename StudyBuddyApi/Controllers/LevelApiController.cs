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
    public class LevelApiController : GenericController<LevelModel, Level>
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

        public HttpResponseMessage Post([FromBody] LevelModel model)
        {
            return Post(model, x => new CreateLevel(x));
        }
    }
}
