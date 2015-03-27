using System.Net.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class PassApiController : GenericApiController<PassModel, Pass>
    {
        public PassApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor, 
            IRepository<Pass> repository, 
            ICommonInterfaceCloner cloner,
            IEntitySearch<Pass> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
        }

        public HttpResponseMessage Put(int id, PassModel model)
        {
            model.Id = id;
            return PerformAction(model, x => new UpdatePass(x));
        }
    }
}