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
    [RoutePrefix("api/levels")]
    public class LevelApiController : GenericApiController<LevelModel, Level>
    {
        public LevelApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<Level> repository,
            IEntitySearch<Level> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post([FromBody] LevelModel model)
        {
            return PerformAction(model, x => new CreateLevel(x));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id,  [FromBody] LevelModel model)
        {
            model.Id = id;
            return PerformAction(model, x => new UpdateLevel(x));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new LevelModel{ Id = id};
            return PerformAction(model, x => new DeleteLevel(x));
        }

        [Route]
        public override HttpResponseMessage Get()
        {
            return base.Get();
        }

        [Route]
        public override HttpResponseMessage Get(string q)
        {
            return base.Get(q);
        }

        [Route("{id:int}")]
        public override HttpResponseMessage Get(int id)
        {
            return base.Get(id);
        }
    }
}
