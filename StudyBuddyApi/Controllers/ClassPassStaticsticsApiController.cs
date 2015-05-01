using System.Collections.Generic;
using System.Net.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
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
}