using ActionHandlers;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassPassStaticsticsApiController : EntityPropertyApiController<ClassPassStaticsticsModel, PassStatisticModel, Class>
    {
        public ClassPassStaticsticsApiController(
            IRepository<Class> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, urlConstructor, cloner, actionHandlerOverlord)
        {
        }
    }
    public class BlockPassStaticsticsApiController : EntityPropertyApiController<ClassPassStaticsticsModel, PassStatisticModel, Class>
    {
        public BlockPassStaticsticsApiController(
            IRepository<Class> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, urlConstructor, cloner, actionHandlerOverlord)
        {
        }
    }
}