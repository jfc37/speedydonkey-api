using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/database")]
    public class DatabaseApiController : ApiController
    {
        private readonly IAppSettings _appSettings;

        public DatabaseApiController(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [Route]
        public HttpResponseMessage Delete()
        {
            if (!Convert.ToBoolean(_appSettings.GetSetting(AppSettingKey.AllowDatabaseDelete)))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["SpeedyDonkeyDbContext"].ConnectionString;

            var sessionSetup = new SessionSetup(connectionString);
            sessionSetup.BuildSchema();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}