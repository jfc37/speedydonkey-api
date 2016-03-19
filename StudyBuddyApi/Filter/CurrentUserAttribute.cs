using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Filter
{
    public class CurrentUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var repository = (IRepository<User>)actionContext.Request.GetDependencyScope().GetService(typeof(IRepository<User>));

            var loggedInUser = new ExtractLoggedInUser(actionContext.Request.GetOwinContext().Authentication.User, repository).Do();

            if (loggedInUser.Any())
            {
                var currentUser = (ICurrentUser)actionContext.Request.GetDependencyScope().GetService(typeof(ICurrentUser));
                currentUser.Id = loggedInUser.Single().Id;
            }
        }
    }
}