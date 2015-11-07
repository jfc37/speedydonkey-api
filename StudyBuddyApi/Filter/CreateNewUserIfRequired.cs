using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SpeedyDonkeyApi.Filter
{
    public class CreateNewUserIfRequired : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //Get user identity
            //If no matching user in db, create one
        }
    }
}