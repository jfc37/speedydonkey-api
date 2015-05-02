using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class PassStatisticApiController : GenericApiController<PassStatisticModel, PassStatistic>
    {
        public PassStatisticApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor, 
            IRepository<PassStatistic> repository, 
            ICommonInterfaceCloner cloner,
            IEntitySearch<PassStatistic> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
        }
    }
}