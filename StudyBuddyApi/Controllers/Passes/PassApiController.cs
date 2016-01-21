using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Contracts;
using Contracts.Passes;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Passes
{
    [RoutePrefix("api/passes")]
    public class PassApiController : GenericApiController<Pass>
    {

        public PassApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<Pass> repository, 
            IEntitySearch<Pass> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Put(int id, ClipPassModel model)
        {
            model.Id = id;

            if (model.PassType == PassType.Unlimited.ToString())
            {
                var unlimitedPassModel = new CommonInterfaceCloner().Clone<ClipPassModel, PassModel>(model);
                var result = PerformAction<UpdatePass, Pass>(new UpdatePass(unlimitedPassModel.ToEntity()));

                return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                    new ActionReponse<PassModel>(result.ActionResult.ToModel(), result.ValidationResult));
            }

            var resultResult = PerformAction<UpdatePass, Pass>(new UpdatePass(model.ToEntity()));

            return Request.CreateResponse(resultResult.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<PassModel>(resultResult.ActionResult.ToModel(), resultResult.ValidationResult));
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new Pass
            {
                Id = id
            };

            var result = PerformAction<DeletePass, Pass>(new DeletePass(model));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<PassModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
    }
}