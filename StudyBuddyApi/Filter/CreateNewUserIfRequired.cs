using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ActionHandlers.CreateHandlers;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using User = Models.User;

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
                    var globalId = new ExtractGlobalIdFromJwt(actionContext.Request.GetOwinContext().Authentication.User).Do();
                    var user = new AuthZeroClientRepository(new AppSettings()).Get(globalId);

                    CreateNewUser(actionContext, user);
                }
            }
        }

        private void CreateNewUser(HttpActionContext actionContext, User authZeroUser)
        {
            var repository = (IRepository<User>)actionContext.Request.GetDependencyScope().GetService(typeof(IRepository<User>));

            authZeroUser.Claims = authZeroUser.Email.IsSameAs("placid.joe@gmail.com")
                ? String.Join(",", new[] {Claim.Admin, Claim.Teacher})
                : "";
            
            repository.Create(authZeroUser);
        }
    }
}