using System.Web.Http;
using Models.OnlinePayments;
using OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;
using SpeedyDonkeyApi.Extensions.Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models.OnlinePayments.PayPal;

namespace SpeedyDonkeyApi.Controllers.OnlinePayments
{
    [RoutePrefix("api/online-payment/paypal")]
    public class PayPalApiController : ApiController
    {
        private readonly IOnlinePaymentManager _onlinePaymentManager;
        private readonly IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse> _startPaymentStrategy;
        private readonly IPaymentStepStrategy<string, PayPalConfirmResponse> _confirmStrategy;
        private readonly IPaymentStepStrategy<string, PayPalCompleteResponse> _completeStrategy;

        public PayPalApiController(
            IOnlinePaymentManager onlinePaymentManager, 
            IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse> startPaymentStrategy,
            IPaymentStepStrategy<string, PayPalConfirmResponse> confirmStrategy,
            IPaymentStepStrategy<string, PayPalCompleteResponse> completeStrategy)
        {
            _onlinePaymentManager = onlinePaymentManager;
            _startPaymentStrategy = startPaymentStrategy;
            _confirmStrategy = confirmStrategy;
            _completeStrategy = completeStrategy;
        }

        [Route("begin")]
        [ValidationActionFilter]
        public IHttpActionResult Post([FromBody] PayPayRequestModel model)
        {
            var response = _onlinePaymentManager.Begin(model.ToRequest(), _startPaymentStrategy, new ResponseCreator<StartPayPalPaymentResponse>());

            return Ok(response);

        }

        [Route("confirm")]
        [ValidationActionFilter]
        public IHttpActionResult Post([FromBody] PayPayConfirmModel model)
        {
            var response = _confirmStrategy.PerformStep(model.Token);

            return Ok(response);

        }

        [Route("complete")]
        [ValidationActionFilter]
        public IHttpActionResult Post([FromBody] PayPayCompleteModel model)
        {
            var response = _completeStrategy.PerformStep(model.Token);

            return Ok(response);

        }
    }
}