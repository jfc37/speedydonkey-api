using System.Web.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
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
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post([FromBody] LevelModel model)
        {
            var result = PerformAction<CreateLevel, Level>(new CreateLevel(model.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<Level, LevelModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Put(int id, [FromBody] LevelModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdateLevel, Level>(new UpdateLevel(model.ToEntity()));

            return new ActionResultToOkHttpActionResult<Level, LevelModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Delete(int id)
        {
            var model = new Level {Id = id};
            var result = PerformAction<DeleteLevel, Level>(new DeleteLevel(model));

            return new ActionResultToOkHttpActionResult<Level, LevelModel>(result, x => x.ToModel(), this)
                .Do();
        }


        [Route]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<Level>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<Level>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<Level>(this, GetById(id), x => x.ToModel()).Do();
        }
    }
}