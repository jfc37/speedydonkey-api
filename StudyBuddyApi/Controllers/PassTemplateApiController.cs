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
    public class PassTemplateApiController : GenericApiController<PassTemplateModel, PassTemplate>
    {
        public PassTemplateApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor, 
            IRepository<PassTemplate> repository, 
            ICommonInterfaceCloner cloner, 
            IEntitySearch<PassTemplate> entitySearch) : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post([FromBody]PassTemplateModel model)
        {
            return PerformAction(model, x => new CreatePassTemplate(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id, [FromBody]PassTemplateModel model)
        {
            model.Id = id;
            return PerformAction(model, x => new UpdatePassTemplate(x));
        }

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