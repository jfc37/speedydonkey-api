using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common;
using Common.Extensions;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Filter
{
    public class CurrentUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            new CreateNewUserIfRequired().OnActionExecuting(actionContext);

            var loggedInUser = new ExtractLoggedInUser(actionContext.Request.GetOwinContext().Authentication.User, actionContext.Request.GetDependencyScope()).Do();

            if (loggedInUser.IsNotNull())
            {
                var currentUser = (ICurrentUser)actionContext.Request.GetDependencyScope().GetService(typeof(ICurrentUser));
                currentUser.Id = loggedInUser.Id;
            }
        }
    }
}