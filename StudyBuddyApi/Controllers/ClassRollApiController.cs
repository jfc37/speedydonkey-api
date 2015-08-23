using ActionHandlers;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassRollApiController : EntityPropertyApiController<ClassRegisterModel, UserModel, Class>
    {
        public ClassRollApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, actionHandlerOverlord)
        {
        }
    }
}