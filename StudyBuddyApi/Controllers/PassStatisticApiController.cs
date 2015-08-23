using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class PassStatisticApiController : GenericApiController<PassStatisticModel, PassStatistic>
    {
        public PassStatisticApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<PassStatistic> repository, 
            IEntitySearch<PassStatistic> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }
    }
}