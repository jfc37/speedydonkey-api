using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Mindscape.Raygun4Net.WebApi;

namespace SpeedyDonkeyApi
{
    public class GlobalErrorLoggerService : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var raygunApiKey = ConfigurationManager.AppSettings.Get("RaygunKey");
            var raygunClient = new RaygunWebApiClient(raygunApiKey);
            raygunClient.Send(context.Exception);

            return Task.FromResult(0);
        }
    }
}