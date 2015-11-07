using System.Linq;
using System.Security.Claims;
using System.Web.Http.Dependencies;
using Data.CodeChunks;
using Data.Repositories;
using Models;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class ExtractLoggedInUser : ICodeChunk<User>
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly IDependencyScope _dependencyScope;

        public ExtractLoggedInUser(ClaimsPrincipal claimsPrincipal, IDependencyScope dependencyScope)
        {
            _claimsPrincipal = claimsPrincipal;
            _dependencyScope = dependencyScope;
        }

        public User Do()
        {
            if (_claimsPrincipal.Identity.IsAuthenticated)
            {
                var globalId = _claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

                var repository = (IRepository<User>)_dependencyScope.GetService(typeof(IRepository<User>));
                return repository.GetAll()
                    .SingleOrDefault(x => x.GlobalId == globalId);
            }

            return null;
        }
    }
}