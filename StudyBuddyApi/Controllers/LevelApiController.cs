using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
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
            IRepository<Level> repository) : base(actionHandlerOverlord, urlConstructor, repository)
        {
        }

        public HttpResponseMessage Post([FromBody] LevelModel model)
        {
            return Post(model, x => new CreateLevel(x));
        }
    }
}
