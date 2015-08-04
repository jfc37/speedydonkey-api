using System.Web;
using System.Web.Http;
using Models.OnlinePayments;
using OnlinePayments;
using OnlinePayments.PaymentMethods.Poli.Models;
using SpeedyDonkeyApi.Extensions.Models;
using SpeedyDonkeyApi.Models.OnlinePayments.Poli;

namespace SpeedyDonkeyApi.Controllers.OnlinePayments
{
    [RoutePrefix("api/online-payment/poli")]
    public class PoliApiController : ApiController
    {
        private readonly IOnlinePaymentManager _onlinePaymentManager;
        private readonly IStartPaymentStrategy<PoliPayment, StartPoliPaymentResponse> _startPaymentStrategy;
        private readonly ICompletePaymentStrategy<string, PoliCompleteResponse, PoliPayment> _completeStrategy;

        public PoliApiController(
            IOnlinePaymentManager onlinePaymentManager,
            IStartPaymentStrategy<PoliPayment, StartPoliPaymentResponse> startPaymentStrategy,
            ICompletePaymentStrategy<string, PoliCompleteResponse, PoliPayment> completeStrategy)
        {
            _onlinePaymentManager = onlinePaymentManager;
            _startPaymentStrategy = startPaymentStrategy;
            _completeStrategy = completeStrategy;
        }

        [Route("begin")]
        public IHttpActionResult Post([FromBody] PoliRequestModel model)
        {
            var response = _onlinePaymentManager.Begin(model.ToRequest(), _startPaymentStrategy, new ResponseCreator<StartPoliPaymentResponse>());

            return Ok(response);

        }

        [Route("complete")]
        public IHttpActionResult Post([FromBody] PoliCompleteModel model)
        {
            var response = _onlinePaymentManager.Complete(model.Token, _completeStrategy);

            return Ok(response);

        }
    }
}