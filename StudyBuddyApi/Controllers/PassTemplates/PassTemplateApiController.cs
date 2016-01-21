using System.Web.Http;
using Action;
using ActionHandlers;
using Contracts;
using Contracts.Passes;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.PassTemplates
{
    [RoutePrefix("api/pass-templates")]
    public class PassTemplateApiController : GenericApiController<PassTemplate>
    {
        public PassTemplateApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<PassTemplate> repository, 
            IEntitySearch<PassTemplate> entitySearch) : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<PassTemplate>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<PassTemplate>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<PassTemplate>(this, GetById(id), x => x.ToModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post([FromBody]PassTemplateModel model)
        {
            var result = PerformAction<CreatePassTemplate, PassTemplate>(new CreatePassTemplate(model.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<PassTemplate, PassTemplateModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Put(int id, [FromBody]PassTemplateModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdatePassTemplate, PassTemplate>(new UpdatePassTemplate(model.ToEntity()));

            return new ActionResultToOkHttpActionResult<PassTemplate, PassTemplateModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Delete(int id)
        {
            var model = new PassTemplate(id);
            var result = PerformAction<DeletePassTemplate, PassTemplate>(new DeletePassTemplate(model));

            return new ActionResultToCreatedHttpActionResult<PassTemplate, PassTemplateModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}