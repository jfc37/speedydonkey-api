using System.Web.Http;
using ActionHandlers;
using Contracts;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Passes
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
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<PassStatistic>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<PassStatistic>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<PassStatistic>(this, GetById(id), x => x.ToModel()).Do();
        }
    }
}