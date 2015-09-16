using ActionHandlers;
using Data.Repositories;
using Models;

namespace SpeedyDonkeyApi.Controllers.Classes
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
}