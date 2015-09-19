using System.Web.Http;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/passes/statistics")]
    public class PassStatisticApiController : GenericApiController<PassStatistic>
    {
        public PassStatisticApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<PassStatistic> repository, 
            IEntitySearch<PassStatistic> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<PassStatistic>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<PassStatistic>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<PassStatistic>(this, GetById(id), x => x.ToModel()).Do();
        }
    }
}