using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Models.OnlinePayments;
using OnlinePayments.Extensions;
using OnlinePayments.Models;
using OnlinePayments.PaymentMethods.PayPal.Models;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public static class DecimalExtensions
    {
        public static string ToCurrencyString(this decimal instance)
        {
            return instance.ToString("N2");
        }
    }

    public static class GetExpressCheckoutDetailsResponseTypeExtensions
    {
        public static PayPalConfirmResponse ToResponse(this GetExpressCheckoutDetailsResponseType instance)
        {
            return new PayPalConfirmResponse
            {
                Token = instance.GetExpressCheckoutDetailsResponseDetails.Token,
                Errors = instance.Errors.Select(x => x.ToPaypalError()),
                Status = instance.Ack.ToString(),
                PayerId = instance.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID
            };
        }
    }
    public static class DoExpressCheckoutDetailsResponseTypeExtensions
    {
        public static PayPalCompleteResponse ToResponse(this DoExpressCheckoutPaymentResponseType instance)
        {
            return new PayPalCompleteResponse
            {
                Errors = instance.Errors.Select(x => x.ToPaypalError()),
                Status = instance.Ack.ToString()
            };
        }
    }

    public interface IExpressCheckout
    {
        StartPayPalPaymentResponse Set(PaymentDetails details);
        PayPalConfirmResponse Get(string token);
        PayPalCompleteResponse Do(PayPalCompleteRequest request);
        StartPayPalPaymentResponse Set(PayPalPayment payment);
    }
    public class ExpressCheckout : IExpressCheckout
    {
        public StartPayPalPaymentResponse Set(PaymentDetails details)
        {
            var request = GetSetRequest(details);

            var wrapper = new SetExpressCheckoutReq {SetExpressCheckoutRequest = request};
            var service = new PayPalAPIInterfaceServiceService(GetConfig());
            SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);
            return setECResponse.ToResponse();
        }

        public StartPayPalPaymentResponse Set(PayPalPayment payment)
        {
            var request = GetSetRequest(payment);

            var wrapper = new SetExpressCheckoutReq { SetExpressCheckoutRequest = request };
            var service = new PayPalAPIInterfaceServiceService(GetConfig());
            SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);
            return setECResponse.ToResponse();
        }

        public PayPalConfirmResponse Get(string token)
        {
            var request = new GetExpressCheckoutDetailsRequestType {Version = "104.0", Token = token};
            var wrapper = new GetExpressCheckoutDetailsReq {GetExpressCheckoutDetailsRequest = request};

            var service = new PayPalAPIInterfaceServiceService(GetConfig());
            GetExpressCheckoutDetailsResponseType ecResponse = service.GetExpressCheckoutDetails(wrapper);

            return ecResponse.ToResponse();
        }

        public PayPalCompleteResponse Do(PayPalCompleteRequest details)
        {
            var paymentDetail = new PaymentDetailsType();
            paymentDetail.PaymentAction = PaymentActionCodeType.SALE;
            paymentDetail.OrderTotal = new BasicAmountType(CurrencyCodeType.NZD, details.Amount.ToCurrencyString());

            var request = new DoExpressCheckoutPaymentRequestType {Version = "104.0"};
            var requestDetails = new DoExpressCheckoutPaymentRequestDetailsType
            {
                PaymentDetails = paymentDetail.PutIntoList(),
                Token = details.Token,
                PayerID = details.PayerId
            };
            request.DoExpressCheckoutPaymentRequestDetails = requestDetails;

            var wrapper = new DoExpressCheckoutPaymentReq {DoExpressCheckoutPaymentRequest = request};
            var service = new PayPalAPIInterfaceServiceService(GetConfig());
            var doECResponse = service.DoExpressCheckoutPayment(wrapper);

            return doECResponse.ToResponse();
        }

        private static SetExpressCheckoutRequestType GetSetRequest(PayPalPayment payment)
        {
            var paymentDetails = GetPaymentDetails(payment);

            var ecDetails = new SetExpressCheckoutRequestDetailsType();
            ecDetails.ReturnURL = payment.ReturnUrl;
            ecDetails.CancelURL = payment.CancelUrl;
            ecDetails.NoShipping = "1";
            ecDetails.ReqConfirmShipping = "0";
            ecDetails.SolutionType = SolutionTypeType.SOLE;
            ecDetails.LandingPage = LandingPageType.BILLING;

            ecDetails.PaymentDetails = paymentDetails;

            var request = new SetExpressCheckoutRequestType();
            request.Version = "104.0";
            request.SetExpressCheckoutRequestDetails = ecDetails;
            return request;
        }

        private static SetExpressCheckoutRequestType GetSetRequest(PaymentDetails details)
        {
            var paymentDetails = GetPaymentDetails(details);

            var ecDetails = new SetExpressCheckoutRequestDetailsType();
            ecDetails.ReturnURL = details.ReturnUrl;
            ecDetails.CancelURL = details.CancelUrl;
            ecDetails.NoShipping = "1";
            ecDetails.ReqConfirmShipping = "0";
            ecDetails.SolutionType = SolutionTypeType.SOLE;
            ecDetails.LandingPage = LandingPageType.BILLING;

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

        private static List<PaymentDetailsType> GetPaymentDetails(PayPalPayment payment)
        {
            var paymentDetail = new PaymentDetailsType();
            var paymentItem = new PaymentDetailsItemType();
            paymentItem.Name = payment.Description;
            paymentItem.Amount = new BasicAmountType(CurrencyCodeType.NZD, payment.Price.ToCurrencyString());
            paymentItem.Quantity = 1;
            //paymentItem.ItemCategory = ItemCategoryType.DIGITAL;
            var paymentItems = new List<PaymentDetailsItemType>();
            paymentItems.Add(paymentItem);
            paymentDetail.PaymentDetailsItem = paymentItems;

            paymentDetail.PaymentAction = PaymentActionCodeType.SALE;
            paymentDetail.OrderTotal = new BasicAmountType(CurrencyCodeType.NZD, payment.Price.ToCurrencyString());
            //paymentDetail.ShippingMethod = ShippingServiceCodeType.DOWNLOAD;
            return paymentDetail.PutIntoList();
        }

        private static List<PaymentDetailsType> GetPaymentDetails(PaymentDetails details)
        {
            var paymentDetail = new PaymentDetailsType();
            var paymentItem = new PaymentDetailsItemType();
            paymentItem.Name = details.Description;
            paymentItem.Amount = new BasicAmountType(CurrencyCodeType.NZD, details.Amount.ToCurrencyString());
            paymentItem.Quantity = 1;
            //paymentItem.ItemCategory = ItemCategoryType.DIGITAL;
            var paymentItems = new List<PaymentDetailsItemType>();
            paymentItems.Add(paymentItem);
            paymentDetail.PaymentDetailsItem = paymentItems;

            paymentDetail.PaymentAction = PaymentActionCodeType.SALE;
            paymentDetail.OrderTotal = new BasicAmountType(CurrencyCodeType.NZD, details.Amount.ToCurrencyString());
            //paymentDetail.ShippingMethod = ShippingServiceCodeType.DOWNLOAD;
            return paymentDetail.PutIntoList();
        }
    }
}
