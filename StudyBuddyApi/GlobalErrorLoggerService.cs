using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Common.Extensions;
using Mindscape.Raygun4Net.Messages;
using Mindscape.Raygun4Net.WebApi;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi
{
    public class GlobalErrorLoggerService : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var raygunApiKey = ConfigurationManager.AppSettings.Get("RaygunKey");
            var raygunClient = new RaygunWebApiClient(raygunApiKey);

            var user = new ExtractLoggedInUser(context.Request.GetOwinContext().Authentication.User, context.Request.GetDependencyScope()).Do();
            if (user.IsNotNull())
            {
                //raygunClient.UserInfo = new RaygunIdentifierMessage(user.Email)
                //{
                //    Email = user.Email,
                //    FirstName = user.FirstName,
                //    FullName = user.FullName,
                //    Identifier = user.Id.ToString(),
                //    IsAnonymous = false
                //};    
            }

            raygunClient.Send(context.Exception);

            return Task.FromResult(0);
        }
    }
}