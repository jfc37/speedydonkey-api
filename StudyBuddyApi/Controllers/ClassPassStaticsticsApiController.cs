using System.Web.Http;
using ActionHandlers;
using Common.Extensions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class ClassPassStaticsticsApiController : EntityPropertyApiController
    {
        private readonly IRepository<Class> _entityRepository;

        public ClassPassStaticsticsApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(actionHandlerOverlord)
        {
            _entityRepository = entityRepository;
        }

        [Route("api/classes/{id:int}/passes/statistics")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            var entity = _entityRepository.Get(id);
            return entity.IsNotNull()
                ? (IHttpActionResult)Ok(new ClassPassStaticsticsModel().ConvertFromEntity(entity))
                : NotFound();
        }
    }
}