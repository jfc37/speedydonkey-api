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
            var claims = GetClaimsForUser(actionContext);
            if  (claims.DoesNotContain(Claim.ToString()))
                HandleUnauthorised(actionContext);
        }
        
        private void HandleUnauthorised(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //TODO: Remove login url hardcoding
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='SpeedyDonkey' location='http://localhost:50831/users/login'");
        }

        private string GetClaimsForUser(HttpActionContext actionContext)
        {
            var loggedInUser = new ExtractLoggedInUser(actionContext.Request).Do();
            return loggedInUser.IsNull() 
                ? "" 
                : loggedInUser.Claims;
        }
    }
}