using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Auth0;
using Common;
using Common.Extensions;
using Data.Repositories;
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
                    var authZeroUser = new AuthZeroUserRetriever(new AppSettings()).GetUser(globalId);

                    CreateNewUser(actionContext, authZeroUser);
                }
            }
        }

        private void CreateNewUser(HttpActionContext actionContext, UserProfile authZeroUser)
        {
            var repository = (IRepository<User>)actionContext.Request.GetDependencyScope().GetService(typeof(IRepository<User>));
            var user = new User
            {
                Email = authZeroUser.Email,
                FirstName = authZeroUser.GivenName,
                Surname = authZeroUser.FamilyName,
                GlobalId = authZeroUser.UserId
            };

            repository.Create(user);
        }
    }

    public class AuthZeroUserRetriever
    {
        private readonly IAppSettings _appSettings;

        public AuthZeroUserRetriever(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public UserProfile GetUser(string globalId)
        {
            var client = new Client(
                clientID: _appSettings.GetSetting(AppSettingKey.AuthZeroClientId),
                clientSecret: _appSettings.GetSetting(AppSettingKey.AuthZeroClientSecret),
                domain: _appSettings.GetSetting(AppSettingKey.AuthZeroDomain)
                );

            return client.GetUser(globalId);
        }
    }
}