using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using log4net;

namespace SpeedyDonkeyApi
{
    public class GlobalErrorLoggerService : IExceptionLogger
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            _log.Error(context.Exception);
            return Task.FromResult(0);
        }
    }
}