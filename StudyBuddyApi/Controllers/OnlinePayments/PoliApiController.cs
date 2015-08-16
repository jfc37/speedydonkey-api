using System.Web.Http;
using Common.Extensions;
using Data;
using Models;
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
        private readonly IActivityLogger _logger;

        public PoliApiController(
            IOnlinePaymentManager onlinePaymentManager,
            IStartPaymentStrategy<PoliPayment, StartPoliPaymentResponse> startPaymentStrategy,
            ICompletePaymentStrategy<string, PoliCompleteResponse, PoliPayment> completeStrategy,
            IActivityLogger logger)
        {
            _onlinePaymentManager = onlinePaymentManager;
            _startPaymentStrategy = startPaymentStrategy;
            _completeStrategy = completeStrategy;
            _logger = logger;
        }

        [Route("begin")]
        public IHttpActionResult Post([FromBody] PoliRequestModel model)
        {
            var response = _onlinePaymentManager.Begin(model.ToRequest(), _startPaymentStrategy, new ResponseCreator<StartPoliPaymentResponse>());

            return Ok(response);

        }

        [Route("nudge")]
        public IHttpActionResult Post([FromBody] PoliNudgeModel model)
        {
            _logger.Log(ActivityGroup.PerformAction, ActivityType.Payment, "Poli Nudge for token".FormatWith(model.Token));

            var response = _onlinePaymentManager.Complete(model.Token, _completeStrategy);

            return Ok(response);

        }

        [Route("complete")]
        public IHttpActionResult Post([FromBody] PoliCompleteModel model)
        {
            _logger.Log(ActivityGroup.PerformAction, ActivityType.Payment, "Poli Complete for token".FormatWith(model.Token));

            var response = _onlinePaymentManager.Complete(model.Token, _completeStrategy);

            return Ok(response);

        }
    }
}