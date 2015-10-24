using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Models;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Filter
{
    public class ActiveUserRequired : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var loggedInUser = new ExtractLoggedInUser(actionContext.Request).Do();
            if (loggedInUser == null || loggedInUser.Status != UserStatus.Active)
                HandleUnauthorised(actionContext);
        }
        
        private void HandleUnauthorised(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //TODO: Remove login url hardcoding
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='SpeedyDonkey' location='http://localhost:50831/users/login'");
        }
    }
}
