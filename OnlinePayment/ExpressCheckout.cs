using System.Collections.Generic;
using Common.Extensions;
using OnlinePayment.Extensions;
using OnlinePayment.Models;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayment
{
    public interface IExpressCheckout
    {
        ExpressCheckoutResponse Set(PaymentDetails details);
    }
    public class ExpressCheckout : IExpressCheckout
    {
        public ExpressCheckoutResponse Set(PaymentDetails details)
        {
            var request = GetSetRequest(details);

            var wrapper = new SetExpressCheckoutReq {SetExpressCheckoutRequest = request};
            var service = new PayPalAPIInterfaceServiceService(GetConfig());
            SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);
            return setECResponse.ToResponse();
        }

        private static SetExpressCheckoutRequestType GetSetRequest(PaymentDetails details)
        {
            var paymentDetails = GetPaymentDetails(details);

            var ecDetails = new SetExpressCheckoutRequestDetailsType();
            ecDetails.ReturnURL = details.ReturnUrl;
            ecDetails.CancelURL = details.CancelUrl;
            ecDetails.PaymentDetails = paymentDetails;

            var request = new SetExpressCheckoutRequestType();
            request.Version = "104.0";
            request.SetExpressCheckoutRequestDetails = ecDetails;
            return request;
        }

        private static Dictionary<string, string> GetConfig()
        {
            var paypalConfig = new Dictionary<string, string>();
            paypalConfig.Add("account1.apiUsername", "placid.joe-1_api1.gmail.com");
            paypalConfig.Add("account1.apiPassword", "2P9K9Q27E8TP5WDM");
            paypalConfig.Add("account1.apiSignature", "AcGfejIZ5oNW4OPYyGJG92z29pNcAoYrSSsNUlCcOE3oWJ2tpC0BCQNO");
            paypalConfig.Add("mode", "sandbox");

            return paypalConfig;
        }

        private static List<PaymentDetailsType> GetPaymentDetails(PaymentDetails details)
        {
            var paymentDetail = new PaymentDetailsType();
            var paymentItem = new PaymentDetailsItemType();
            paymentItem.Name = details.Description;
            paymentItem.Amount = new BasicAmountType(CurrencyCodeType.NZD, details.Amount.ToString());
            paymentItem.Quantity = 1;
            paymentItem.ItemCategory = ItemCategoryType.DIGITAL;
            var paymentItems = new List<PaymentDetailsItemType>();
            paymentItems.Add(paymentItem);
            paymentDetail.PaymentDetailsItem = paymentItems;

            paymentDetail.PaymentAction = PaymentActionCodeType.SALE;
            paymentDetail.OrderTotal = new BasicAmountType(CurrencyCodeType.NZD, details.Amount.ToString());

            return paymentDetail.ToList();
        }
    }
}
