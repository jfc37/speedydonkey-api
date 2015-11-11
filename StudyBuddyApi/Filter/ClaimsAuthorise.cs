using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Extensions;
using Models;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Filter
{
    public class ClaimsAuthorise : AuthorizationFilterAttribute
    {
        public Claim Claim { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            new CreateNewUserIfRequired().OnActionExecuting(actionContext);

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
            var loggedInUser = new ExtractLoggedInUser(actionContext.Request.GetOwinContext().Authentication.User, actionContext.Request.GetDependencyScope()).Do();
            return loggedInUser.IsNull() || loggedInUser.Claims.IsNullOrWhiteSpace() 
                ? "" 
                : loggedInUser.Claims;
        }
    }
}