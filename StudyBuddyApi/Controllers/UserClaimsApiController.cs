using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common;
using Common.Extensions;
using Data.CodeChunks;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
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
        [ActiveUserRequired]
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

    public class ExtractUserClaims : ICodeChunk<IEnumerable<string>>
    {
        private readonly User _user;

        public ExtractUserClaims(User user)
        {
            _user = user;
        }

        public IEnumerable<string> Do()
        {
            return _user.IsNull() || _user.Claims.IsNullOrWhiteSpace()
                ? null
                : _user.Claims.Split(',').ToList();
        }
    }
}