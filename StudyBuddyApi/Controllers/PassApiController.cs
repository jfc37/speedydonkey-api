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
        private readonly ICommonInterfaceCloner _cloner;

        public PassApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor, 
            IRepository<Pass> repository, 
            ICommonInterfaceCloner cloner,
            IEntitySearch<Pass> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
            _cloner = cloner;
        }

        public HttpResponseMessage Put(int id, ClipPassModel model)
        {
            model.Id = id;

            if (model.PassType == PassType.Unlimited.ToString())
            {
                var unlimitedPassModel = _cloner.Clone<ClipPassModel, PassModel>(model);
                return PerformAction(unlimitedPassModel, x => new UpdatePass(x));
            }

            return PerformAction(model, x => new UpdatePass(x));
        }
    }
}