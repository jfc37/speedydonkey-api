using System.Web.Http;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Users
{
    public class UserClaimsApiController : BaseApiController
    {
        private readonly IRepository<User> _repository;
        private readonly ICurrentUser _currentUser;

        public UserClaimsApiController(
            IRepository<User> repository,
            ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        [Route("api/users/current/claims")]
        public IHttpActionResult Get()
        {
            return Get(_currentUser.Id);
        }

        [Route("api/users/{id:int}/claims")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            var claims = new ExtractUserClaims(_repository.Get(id)).Do();
            return claims.IsNotNull()
                ? (IHttpActionResult) Ok(claims)
                : NotFound();
        }
    }
}