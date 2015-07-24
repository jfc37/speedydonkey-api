//using System.Web.Http;
//using Action.OnlinePayment;
//using ActionHandlers;
//using Common;
//using Models;
//using OnlinePayments;
//using OnlinePayments.Models;
//using SpeedyDonkeyApi.Models;

//namespace SpeedyDonkeyApi.Controllers
//{
//    public class PayPalApiController : BaseApiController
//    {
//        private readonly IActionHandlerOverlord _actionHandlerOverlord;
//        private readonly ICurrentUser _currentUser;

//        public PayPalApiController(IActionHandlerOverlord actionHandlerOverlord, ICurrentUser currentUser)
//        {
//            _actionHandlerOverlord = actionHandlerOverlord;
//            _currentUser = currentUser;
//        }

//        [Route("api/paypal/begin")]
//        public IHttpActionResult Begin(PayPalBeginViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var response = _actionHandlerOverlord.HandleAction<BeginOnlinePayment, PendingOnlinePayment, StartPayPalPaymentResponse>(model.ToAction(_currentUser));

//            return response.ValidationResult.IsValid
//                ? (IHttpActionResult)Ok(response)
//                : BadRequest(response.ValidationResult.ToModelState());

//        }

//        [Route("api/paypal/confirm")]
//        public IHttpActionResult Confirm(PayPalConfirmViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var response = _actionHandlerOverlord.HandleAction<ConfirmOnlinePayment, PendingOnlinePayment, PayPalConfirmResponse>(new ConfirmOnlinePayment(model.Token));

//            return response.ValidationResult.IsValid
//                ? (IHttpActionResult)Ok(response)
//                : BadRequest(response.ValidationResult.ToModelState());
//        }

//        [Route("api/paypal/complete")]
//        public IHttpActionResult Complete(PayPalConfirmViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var response = _actionHandlerOverlord.HandleAction<CompleteOnlinePayment, PendingOnlinePayment, PayPalCompleteResponse>(new CompleteOnlinePayment(model.Token));

//            return response.ValidationResult.IsValid
//                ? (IHttpActionResult)Ok(response)
//                : BadRequest(response.ValidationResult.ToModelState());
//        }

//    }
//}