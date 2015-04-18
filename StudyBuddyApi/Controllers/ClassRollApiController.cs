using ActionHandlers;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassRollApiController : EntityPropertyApiController<ClassRegisterModel, UserModel, Class>
    {
        public ClassRollApiController(
            IRepository<Class> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, urlConstructor, cloner, actionHandlerOverlord)
        {
        }
    }
}