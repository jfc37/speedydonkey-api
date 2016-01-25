using System.Web.Http;
using Action.OpeningHours;
using ActionHandlers;
using Contracts.MappingExtensions;
using Contracts.PrivateLessons;
using Data.Repositories;
using Data.Searches;
using Models.PrivateLessons;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Controllers.PrivateLessons
{
    [RoutePrefix("api/opening-hours")]
    public class OpeningHoursApiController : GenericApiController<OpeningHours>
    {
        public OpeningHoursApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<OpeningHours> repository, 
            IEntitySearch<OpeningHours> entitySearch) 
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<OpeningHours>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        //[ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post([FromBody]OpeningHoursModel model)
        {
            var result = PerformAction<SetOpeningHours, OpeningHours>(new SetOpeningHours(model.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<OpeningHours, OpeningHoursModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}
