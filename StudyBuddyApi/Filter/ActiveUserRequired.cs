﻿using System;
using System.Collections.Generic;
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
using Data.Searches;
using Models;

namespace SpeedyDonkeyApi.Filter
{
    public class ActiveUserRequired : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = GetUser(actionContext);
            if (user == null || user.Status != UserStatus.Active)
                HandleUnauthorised(actionContext);
        }
        
        private void HandleUnauthorised(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //TODO: Remove login url hardcoding
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='SpeedyDonkey' location='http://localhost:50831/users/login'");
        }

        private User GetUser(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    !String.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var email = GetEmail(authHeader);
                    var q = String.Format("{0}{1}{2}{3}{4}", SearchElements.Email, SearchSyntax.Seperator, SearchKeyWords.Equals, SearchSyntax.Seperator, email);
                    //var userSearch = (IEntitySearch<User>)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IEntitySearch<User>));
                    var userSearch = (IEntitySearch<User>)actionContext.Request.GetDependencyScope().GetService(typeof(IEntitySearch<User>));
                    var user = userSearch.Search(q).SingleOrDefault();
                    return user;
                }
            }
            return null;
        }

        private string GetEmail(AuthenticationHeaderValue authHeader)
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
                return "";
            }
            var split = credentials.Split(':');
            if (split.Count() != 2)
                return "";
            return split[0];
        }
    }
}