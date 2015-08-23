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

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassApiController : GenericApiController<ClassModel, Class>
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
            return PerformAction(model, x => new UpdateClass(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new ClassModel { Id = id };
            return PerformAction(model, x => new DeleteClass(x));
        }
    }
}