using System.Web.Http;
using Models.OnlinePayments;
using OnlinePayments;
using SpeedyDonkeyApi.Extensions.Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models.OnlinePayments.BankDeposit;

namespace SpeedyDonkeyApi.Controllers.OnlinePayments
{
    [RoutePrefix("api/online-payment/bank-deposit")]
    public class BankDepositApiController : ApiController
    {
        private readonly IOnlinePaymentManager _onlinePaymentManager;
        private readonly IStartPaymentStrategy<OnlinePayment, StartBankDepositPaymentResponse> _startPaymentStrategy;

        public BankDepositApiController(
            IOnlinePaymentManager onlinePaymentManager,
            IStartPaymentStrategy<OnlinePayment, StartBankDepositPaymentResponse> startPaymentStrategy)
        {
            _onlinePaymentManager = onlinePaymentManager;
            _startPaymentStrategy = startPaymentStrategy;
        }

        [Route("begin")]
        [ValidationActionFilter]
        public IHttpActionResult Post([FromBody] BankDepositRequestModel model)
        {
            var response = _onlinePaymentManager.Begin(model.ToRequest(), _startPaymentStrategy, new ResponseCreator<StartBankDepositPaymentResponse>());

            return Ok(response);
        }
    }
}