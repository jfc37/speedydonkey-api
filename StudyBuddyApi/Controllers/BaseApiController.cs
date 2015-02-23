using System.Web.Http;
using System.Web.Http.Cors;

namespace SpeedyDonkeyApi.Controllers
{
    [EnableCors(origins: "http://spa-speedydonkey.azurewebsites.net,http://localhost:7300", headers: "*", methods: "*")]
    public abstract class BaseApiController : ApiController
    {
    }
}
