using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using log4net;
using Models;

namespace SpeedyDonkeyApi
{
    public class GlobalErrorLoggerService : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var logger = LogManager.GetLogger(typeof(Class));
            logger.Error("Error", context.Exception);
            
            return Task.FromResult(0);
        }
    }
}