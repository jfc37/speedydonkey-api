using System.Net.Http;
using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class PassApiController : GenericController<PassModel, Pass>
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
    }
}