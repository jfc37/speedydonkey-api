using System.Linq;
using System.Security.Claims;
using Common;
using Data.Repositories;
using Models;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class ExtractLoggedInUser
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly IRepository<User> _repository;

        public ExtractLoggedInUser(
            ClaimsPrincipal claimsPrincipal, 
            IRepository<User> repository)
        {
            _claimsPrincipal = claimsPrincipal;
            _repository = repository;
        }

        public Option<User> Do()
        {
            if (!_claimsPrincipal.Identity.IsAuthenticated)
            {
                return Option<User>.None();
            }

            var globalId = new ExtractGlobalIdFromJwt(_claimsPrincipal).Do();

            return _repository.Queryable()
                .Where(x => x.GlobalId == globalId)
                .Select(x => Option<User>.Some(x))
                .ToList()
                .DefaultIfEmpty(Option<User>.None())
                .Single();
        }
    }
}