using System.Web.Http;
using Action.OnlinePayment;
using ActionHandlers;
using Common;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class PayPalApiController : BaseApiController
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly ICurrentUser _currentUser;

        public PayPalApiController(IActionHandlerOverlord actionHandlerOverlord, ICurrentUser currentUser)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            _currentUser = currentUser;
        }

        public IHttpActionResult Post(PayPalBeginViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

           var response = _actionHandlerOverlord.HandleAction<BeginOnlinePayment, PendingOnlinePayment>(model.ToAction(_currentUser));

            return response.ValidationResult.IsValid
                ? (IHttpActionResult) Ok(response)
                : BadRequest(response.ValidationResult.ToModelState());

        }
    }
}