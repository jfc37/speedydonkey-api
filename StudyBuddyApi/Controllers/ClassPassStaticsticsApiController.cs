using ActionHandlers;
using Common;
using Data.Repositories;
using Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassPassStaticsticsApiController : EntityPropertyApiController
    {
        public ClassPassStaticsticsApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(actionHandlerOverlord)
        {
        }
    }
    public class BlockPassStaticsticsApiController : EntityPropertyApiController
    {
        public BlockPassStaticsticsApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(actionHandlerOverlord)
        {
        }
    }
}