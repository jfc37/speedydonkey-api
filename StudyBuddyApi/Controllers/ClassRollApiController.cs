using ActionHandlers;
using Common;
using Data.Repositories;
using Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassRollApiController : EntityPropertyApiController
    {
        public ClassRollApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(actionHandlerOverlord)
        {
        }
    }
}