using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Extensions;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Filter
{
    public class CreateNewUserIfRequired : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            if (actionContext.Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
            {
                //Get user identity
                var loggedInUser = new ExtractLoggedInUser(actionContext.Request.GetOwinContext().Authentication.User, actionContext.Request.GetDependencyScope()).Do();

                //If no matching user in db, create one
                if (loggedInUser.IsNull())
                {

                }
            }
        }
    }
}