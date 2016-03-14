using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Extensions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Filter
{
    public class ClaimsAuthorise : AuthorizationFilterAttribute
    {
        public Claim Claim { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var claims = GetClaimsForUser(actionContext);
            if  (claims.DoesNotContain(Claim.ToString()))
                HandleUnauthorised(actionContext);
        }
        
        private void HandleUnauthorised(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        private string GetClaimsForUser(HttpActionContext actionContext)
        {
            var repository = (IRepository<User>)actionContext.Request.GetDependencyScope().GetService(typeof (IRepository<User>));

            var loggedInUser = new ExtractLoggedInUser(actionContext.Request.GetOwinContext().Authentication.User, repository)
                .Do();

            return loggedInUser.NotAny() || loggedInUser.Single().Claims.IsNullOrWhiteSpace() 
                ? "" 
                : loggedInUser.Single().Claims;
        }
    }
}