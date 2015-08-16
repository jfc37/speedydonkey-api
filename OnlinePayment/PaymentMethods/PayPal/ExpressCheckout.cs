using Common;
using Models.OnlinePayments;
using OnlinePayments.CodeChunks;
using OnlinePayments.Extensions;
using OnlinePayments.PaymentMethods.PayPal.Extensions;
using OnlinePayments.PaymentMethods.PayPal.Models;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public class ExpressCheckout : IExpressCheckout
    {
        private readonly IAppSettings _appSettings;

        public ExpressCheckout(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public StartPayPalPaymentResponse Set(PayPalPayment payment)
        {
            var request = new GetSetExpressCheckoutReqFromPayment(payment).Do();
            var service = new GetPayPalAPIInterfaceServiceService(_appSettings).Do();

            var setExpressCheckoutResponse = service.SetExpressCheckout(request);

            return setExpressCheckoutResponse.ToResponse();
        }

        public PayPalConfirmResponse Get(string token)
        {
            var request = new GetExpressCheckoutDetailsReqFromPayment(token).Do();
            var service = new GetPayPalAPIInterfaceServiceService(_appSettings).Do();

            var expressCheckoutResponse = service.GetExpressCheckoutDetails(request);

            return expressCheckoutResponse.ToResponse();
        }

        public PayPalCompleteResponse Do(PayPalCompleteRequest details)
        {
            var request = new GetDoExpressCheckoutPaymentReq(details).Do();
            var service = new GetPayPalAPIInterfaceServiceService(_appSettings).Do();
            
            var doExpressCheckoutResponse = service.DoExpressCheckoutPayment(request);

            return doExpressCheckoutResponse.ToResponse();
        }
    }
}
