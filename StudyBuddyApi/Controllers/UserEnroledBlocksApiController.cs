using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Common;
using Common.Extensions;
using Data.Repositories;
using FluentNHibernate.Conventions;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserEnroledBlocksApiController : EntityPropertyApiController
    {
        private readonly IRepository<User> _entityRepository;
        private readonly ICurrentUser _currentUser;

        public UserEnroledBlocksApiController(
            IRepository<User> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord,
            ICurrentUser currentUser)
            : base(actionHandlerOverlord)
        {
            _entityRepository = entityRepository;
            _currentUser = currentUser;
        }

        [ActiveUserRequired]
        public IHttpActionResult Get()
        {
            return Get(_currentUser.Id);
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            var entity = _entityRepository.Get(id);
            return entity.IsNotNull()
                ? (IHttpActionResult) Ok(new UserEnroledBlocksModel().ConvertFromEntity(entity))
                : NotFound();
        }
    }
}