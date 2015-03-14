using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpeedyDonkeyApi.Controllers
{
    public class DatabaseApiController : ApiController
    {
        public HttpResponseMessage Delete()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SpeedyDonkeyDbContext"].ConnectionString;

            var sessionSetup = new SessionSetup(connectionString);
            sessionSetup.BuildSchema();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}