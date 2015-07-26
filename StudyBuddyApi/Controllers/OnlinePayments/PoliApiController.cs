using System.Web.Http;
using Models.OnlinePayments;
using OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;
using OnlinePayments.PaymentMethods.Poli.Models;
using SpeedyDonkeyApi.Extensions.Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models.OnlinePayments.PayPal;
using SpeedyDonkeyApi.Models.OnlinePayments.Poli;

namespace SpeedyDonkeyApi.Controllers.OnlinePayments
{
    [RoutePrefix("api/online-payment/poli")]
    public class PoliApiController : ApiController
    {
        private readonly IOnlinePaymentManager _onlinePaymentManager;
        private readonly IStartPaymentStrategy<PoliPayment, StartPoliPaymentResponse> _startPaymentStrategy;
        private readonly IPaymentStepStrategy<string, PayPalConfirmResponse> _confirmStrategy;
        private readonly IPaymentStepStrategy<string, PayPalCompleteResponse> _completeStrategy;

        public PoliApiController(
            IOnlinePaymentManager onlinePaymentManager,
            IStartPaymentStrategy<PoliPayment, StartPoliPaymentResponse> startPaymentStrategy,
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
        public IHttpActionResult Post([FromBody] PoliRequestModel model)
        {
            var response = _onlinePaymentManager.Begin(model.ToRequest(), _startPaymentStrategy, new ResponseCreator<StartPoliPaymentResponse>());

            return Ok(response);

        }

        [Route("confirm")]
        [ValidationActionFilter]
        public IHttpActionResult Post([FromBody] PayPalConfirmModel model)
        {
            var response = _confirmStrategy.PerformStep(model.Token);

            return Ok(response);

        }

        [Route("complete")]
        [ValidationActionFilter]
        public IHttpActionResult Post([FromBody] PayPalCompleteModel model)
        {
            var response = _completeStrategy.PerformStep(model.Token);

            return Ok(response);

        }
    }
}