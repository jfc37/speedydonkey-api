using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common.Extensions;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/levels")]
    public class LevelApiController : GenericApiController<Level>
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
            var result = PerformAction<CreateLevel, Level>(new CreateLevel(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<LevelModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id,  [FromBody] LevelModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdateLevel, Level>(new UpdateLevel(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<LevelModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new Level{ Id = id};
            var result = PerformAction<DeleteLevel, Level>(new DeleteLevel(model));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<LevelModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
          

        [Route]
        public IHttpActionResult Get()
        {
            var all = GetAll().ToList();
            return all.Any()
                ? (IHttpActionResult) Ok(all.Select(x => x.ToModel()))
                : NotFound();
        }

        [Route]
        public IHttpActionResult Get(string q)
        {
            var all = Search(q).ToList();
            return all.Any()
                ? (IHttpActionResult)Ok(all.Select(x => x.ToModel()))
                : NotFound();
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var result = GetById(id);
            return result.IsNotNull()
                ? (IHttpActionResult) Ok(result.ToModel())
                : NotFound();
        }
    }
}
