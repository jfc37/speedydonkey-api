using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ActionHandlers;
using Data.Searches;
using Models;

namespace SpeedyDonkeyApi.Filter
{
    public class BasicAuthAuthoriseAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                return;

            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && !String.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var credentials = GetCredentials(authHeader);
                    var username = credentials[0];
                    var password = credentials[1];

                    if (AreCredentialsCorrect(username, password))
                    {
                        var principal = new GenericPrincipal(new GenericIdentity(username), null);
                        Thread.CurrentPrincipal = principal;
                        return;
                    }
                }
            }

            HandleUnauthorised(actionContext);
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
                return new []{"", ""};
            }
            var split = credentials.Split(':');
            if (split.Count() != 2)
                return new[] { "", "" };
            return split;
        }

        private bool AreCredentialsCorrect(string username, string password)
        {
            if (!String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password))
            {
                var q = String.Format("{0}{1}{2}{3}{4}", SearchElements.Email, SearchSyntax.Seperator, SearchKeyWords.Equals, SearchSyntax.Seperator, username);
                var userSearch = (IEntitySearch<Account>)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IEntitySearch<Account>));
                var user = userSearch.Search(q).SingleOrDefault();
                if (user != null)
                {
                    var passwordHasher = (IPasswordHasher) GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IPasswordHasher));
                    return passwordHasher.ValidatePassword(password, user.Password);
                }
            }

            return false;
        }

        private void HandleUnauthorised(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //TODO: Remove login url hardcoding
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='SpeedyDonkey' location='http://localhost:50831/users/login'");
        }
    }
}