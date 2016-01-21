using System.Web.Http;
using ActionHandlers;
using Common.Extensions;
using Contracts.Classes;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Classes
{
    [RoutePrefix("api/classes")]
    public class ClassRollApiController : EntityPropertyApiController
    {
        private readonly IRepository<Class> _entityRepository;

        public ClassRollApiController(
            IRepository<Class> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(actionHandlerOverlord)
        {
            _entityRepository = entityRepository;
        }

        [Route("{id:int}/roll")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            var entity = _entityRepository.Get(id);
            return entity.IsNotNull()
                ? (IHttpActionResult)Ok(new ClassRegisterModel().ConvertFromEntity(entity))
                : NotFound();
        }
    }
}