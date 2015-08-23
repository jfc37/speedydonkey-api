using ActionHandlers;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassPassStaticsticsApiController : EntityPropertyApiController<ClassPassStaticsticsModel, PassStatisticModel, Class>
    {
        public ClassPassStaticsticsApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, actionHandlerOverlord)
        {
        }
    }
    public class BlockPassStaticsticsApiController : EntityPropertyApiController<ClassPassStaticsticsModel, PassStatisticModel, Class>
    {
        public BlockPassStaticsticsApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, actionHandlerOverlord)
        {
        }
    }
}