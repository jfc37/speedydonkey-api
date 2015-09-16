using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class PassStatisticApiController : GenericApiController<PassStatistic>
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