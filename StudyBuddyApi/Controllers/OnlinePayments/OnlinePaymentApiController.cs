using System.Web.Http;
using Models.OnlinePayments;
using OnlinePayments;
using OnlinePayments.Models;
using SpeedyDonkeyApi.Extensions.Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models.OnlinePayments;

namespace SpeedyDonkeyApi.Controllers.OnlinePayments
{
    public class PayPalApiController : ApiController
    {
        private readonly IOnlinePaymentManager _onlinePaymentManager;
        private readonly IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse> _startPaymentStrategy;
        private readonly IPaymentStepStrategy<string, PayPalConfirmResponse> _confirmStrategy;

        public PayPalApiController(
            IOnlinePaymentManager onlinePaymentManager, 
            IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse> startPaymentStrategy,
            IPaymentStepStrategy<string, PayPalConfirmResponse> confirmStrategy)
        {
            _onlinePaymentManager = onlinePaymentManager;
            _startPaymentStrategy = startPaymentStrategy;
            _confirmStrategy = confirmStrategy;
        }

        //[Route("api/online-payment/paypal/begin")]
        [ValidationActionFilter]
        public IHttpActionResult Begin([FromBody] PayPayRequestModel model)
        {
            var response = _onlinePaymentManager.Begin(model.ToRequest(), _startPaymentStrategy);

            return Ok(response);

        }

        //[Route("api/online-payment/paypal/begin")]
        [ValidationActionFilter]
        public IHttpActionResult Confirm([FromBody] PayPayConfirmModel model)
        {
            var response = _confirmStrategy.PerformStep(model.Token);

            return Ok(response);

        }
    }
}