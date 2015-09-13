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
    [RoutePrefix("api/pass-templates")]
    public class PassTemplateApiController : GenericApiController<PassTemplate>
    {
        public PassTemplateApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<PassTemplate> repository, 
            IEntitySearch<PassTemplate> entitySearch) : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        public IHttpActionResult Get()
        {
            var all = GetAll().ToList();

            return all.Any()
                ? (IHttpActionResult) Ok(all.Select(x => x.ToModel()))
                : NotFound();
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var entity = GetById(id);

            return entity.IsNotNull()
                ? (IHttpActionResult) Ok(entity.ToModel())
                : NotFound();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post([FromBody]PassTemplateModel model)
        {
            var result = PerformAction<CreatePassTemplate, PassTemplate>(new CreatePassTemplate(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<PassTemplateModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id, [FromBody]PassTemplateModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdatePassTemplate, PassTemplate>(new UpdatePassTemplate(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<PassTemplateModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new PassTemplate
            {
                Id = id
            };
            var result = PerformAction<DeletePassTemplate, PassTemplate>(new DeletePassTemplate(model));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<PassTemplateModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
    }
}