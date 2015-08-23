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
    [RoutePrefix("api/passtemplate")]
    public class PassTemplateApiController : GenericApiController<PassTemplateModel, PassTemplate>
    {
        public PassTemplateApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<PassTemplate> repository, 
            IEntitySearch<PassTemplate> entitySearch) : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post([FromBody]PassTemplateModel model)
        {
            return PerformAction(model, x => new CreatePassTemplate(x));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id, [FromBody]PassTemplateModel model)
        {
            model.Id = id;
            return PerformAction(model, x => new UpdatePassTemplate(x));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new PassTemplateModel
            {
                Id = id
            };
            return PerformAction(model, x => new DeletePassTemplate(x));
        }
    }
}