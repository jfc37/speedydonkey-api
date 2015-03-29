using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{
    public class DatabaseApiController : ApiController
    {

        //[ClaimsAuthorise(Claim = Claim.DeleteDatabase)]
        public HttpResponseMessage Delete()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SpeedyDonkeyDbContext"].ConnectionString;

            var sessionSetup = new SessionSetup(connectionString);
            sessionSetup.BuildSchema();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}