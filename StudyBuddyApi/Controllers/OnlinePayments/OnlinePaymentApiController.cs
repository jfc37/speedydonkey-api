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

        public PayPalApiController(IOnlinePaymentManager onlinePaymentManager, IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse> startPaymentStrategy)
        {
            _onlinePaymentManager = onlinePaymentManager;
            _startPaymentStrategy = startPaymentStrategy;
        }

        //[Route("api/online-payment/paypal/begin")]
        [ValidationActionFilter]
        public IHttpActionResult Post([FromBody] PayPayRequestModel model)
        {
            var response = _onlinePaymentManager.Begin(model.ToRequest(), _startPaymentStrategy);

            return Ok(response);

        }
    }
}