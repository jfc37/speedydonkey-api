using System.Net;
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
    public class ClassApiController : GenericApiController<Class>
    {
        public ClassApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<Class> repository, 
            IEntitySearch<Class> entitySearch) : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Put(int id, [FromBody] ClassModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdateClass, Class>(new UpdateClass(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<ClassModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new Class { Id = id };
            var result = PerformAction<DeleteClass, Class>(new DeleteClass(model));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<ClassModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
    }
}