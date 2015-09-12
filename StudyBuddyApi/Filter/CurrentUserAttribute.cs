using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ActionHandlers;
using Common;
using Data.Searches;
using Models;

namespace SpeedyDonkeyApi.Filter
{
    public class CurrentUserActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && !String.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var credentials = GetCredentials(authHeader);
                    var username = credentials[0];
                    var password = credentials[1];

                    AreCredentialsCorrect(username, password, actionContext);
                }
            }
        }

        private string[] GetCredentials(AuthenticationHeaderValue authHeader)
        {
            var rawCredentials = authHeader.Parameter;
            var encoding = Encoding.GetEncoding("iso-8859-1");
            string credentials;
            try
            {
                credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
            }
            catch (FormatException)
            {
                return new[] { "", "" };
            }
            var split = credentials.Split(':');
            if (split.Count() != 2)
                return new[] { "", "" };
            return split;
        }

        private bool AreCredentialsCorrect(string username, string password, HttpActionContext actionContext)
        {
            if (!String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password))
            {
                var q = String.Format("{0}{1}{2}{3}{4}", SearchElements.Email, SearchSyntax.Seperator, SearchKeyWords.Equals, SearchSyntax.Seperator, username);
                //var userSearch = (IEntitySearch<User>)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IEntitySearch<User>));
                var userSearch = (IEntitySearch<User>)actionContext.Request.GetDependencyScope().GetService(typeof(IEntitySearch<User>));
                var user = userSearch.Search(q).SingleOrDefault();
                if (user != null)
                {

                    //var passwordHasher = (PasswordHasher) GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(PasswordHasher));
                    var passwordHasher = (PasswordHasher)actionContext.Request.GetDependencyScope().GetService(typeof(PasswordHasher));
                    if (passwordHasher.ValidatePassword(password, user.Password))
                    {
                        SetCurrentUser(actionContext, user);
                    }
                }
            }

            return false;
        }

        private void SetCurrentUser(HttpActionContext actionContext, User user)
        {
            try
            {
                var currentUser = (ICurrentUser)actionContext.Request.GetDependencyScope().GetService(typeof(ICurrentUser));
                currentUser.Id = user.Id;
            }
            catch (Exception)
            {
            }
        }

    }
}