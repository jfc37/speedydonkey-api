using System.Web.Http;
using Models.OnlinePayments;
using OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;
using SpeedyDonkeyApi.Extensions.Models;
using SpeedyDonkeyApi.Models.OnlinePayments.PayPal;

namespace SpeedyDonkeyApi.Controllers.OnlinePayments
{
    [RoutePrefix("api/online-payment/paypal")]
    public class PayPalApiController : ApiController
    {
        private readonly IOnlinePaymentManager _onlinePaymentManager;
        private readonly IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse> _startPaymentStrategy;
        private readonly IPaymentStepStrategy<string, PayPalConfirmResponse> _confirmStrategy;
        private readonly ICompletePaymentStrategy<string, PayPalCompleteResponse, PayPalPayment> _completeStrategy;

        public PayPalApiController(
            IOnlinePaymentManager onlinePaymentManager, 
            IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse> startPaymentStrategy,
            IPaymentStepStrategy<string, PayPalConfirmResponse> confirmStrategy,
            ICompletePaymentStrategy<string, PayPalCompleteResponse, PayPalPayment> completeStrategy)
        {
            _onlinePaymentManager = onlinePaymentManager;
            _startPaymentStrategy = startPaymentStrategy;
            _confirmStrategy = confirmStrategy;
            _completeStrategy = completeStrategy;
        }

        [Route("begin")]
        public IHttpActionResult Post([FromBody] PayPalRequestModel model)
        {
            var response = _onlinePaymentManager.Begin(model.ToRequest(), _startPaymentStrategy, new ResponseCreator<StartPayPalPaymentResponse>());

            return Ok(response);

        }

        [Route("confirm")]
        public IHttpActionResult Post([FromBody] PayPalConfirmModel model)
        {
            var response = _confirmStrategy.PerformStep(model.Token);

            return Ok(response);

        }

        [Route("complete")]
        public IHttpActionResult Post([FromBody] PayPalCompleteModel model)
        {
            var response = _onlinePaymentManager.Complete(model.Token, _completeStrategy);

            return Ok(response);

        }
    }
}