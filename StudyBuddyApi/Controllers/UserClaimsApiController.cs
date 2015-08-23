using System.Net.Http;
using ActionHandlers;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserClaimsApiController : EntityPropertyApiController<UserClaimsModel, string, User>
    {
        private readonly ICurrentUser _currentUser;

        public UserClaimsApiController(
            IRepository<User> entityRepository, 
            IActionHandlerOverlord actionHandlerOverlord,
            ICurrentUser currentUser)
            : base(entityRepository, actionHandlerOverlord)
        {
            _currentUser = currentUser;
        }

        [ActiveUserRequired]
        public HttpResponseMessage Get()
        {
            return Get(_currentUser.Id);
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public override HttpResponseMessage Get(int id)
        {
            return base.Get(id);
        }
    }
}