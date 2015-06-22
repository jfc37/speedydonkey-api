using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using OnlinePayment.Extensions;
using OnlinePayment.Models;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayment
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
        public static GetExpressCheckoutResponse ToResponse(this GetExpressCheckoutDetailsResponseType instance)
        {
            return new GetExpressCheckoutResponse
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
        public static DoExpressCheckoutResponse ToResponse(this DoExpressCheckoutPaymentResponseType instance)
        {
            return new DoExpressCheckoutResponse
            {
                Errors = instance.Errors.Select(x => x.ToPaypalError()),
                Status = instance.Ack.ToString()
            };
        }
    }

    public class GetExpressCheckoutResponse
    {
        public string Token { get; set; }
        public IEnumerable<PaypalError> Errors { get; set; }
        public string Status { get; set; }
        public string PayerId { get; set; }
    }

    public class DoExpressCheckoutResponse
    {
        public string Token { get; set; }
        public IEnumerable<PaypalError> Errors { get; set; }
        public string Status { get; set; }
        public string PayerId { get; set; }
    }

    public class DoExpressCheckoutRequest
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
        public string PayerId { get; set; }
    }

    public interface IExpressCheckout
    {
        SetExpressCheckoutResponse Set(PaymentDetails details);
        GetExpressCheckoutResponse Get(string token);
        DoExpressCheckoutResponse Do(DoExpressCheckoutRequest request);
    }
    public class ExpressCheckout : IExpressCheckout
    {
        public SetExpressCheckoutResponse Set(PaymentDetails details)
        {
            var request = GetSetRequest(details);

            var wrapper = new SetExpressCheckoutReq {SetExpressCheckoutRequest = request};
            var service = new PayPalAPIInterfaceServiceService(GetConfig());
            SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);
            return setECResponse.ToResponse();
        }

        public GetExpressCheckoutResponse Get(string token)
        {
            var request = new GetExpressCheckoutDetailsRequestType {Version = "104.0", Token = token};
            var wrapper = new GetExpressCheckoutDetailsReq {GetExpressCheckoutDetailsRequest = request};

            var service = new PayPalAPIInterfaceServiceService(GetConfig());
            GetExpressCheckoutDetailsResponseType ecResponse = service.GetExpressCheckoutDetails(wrapper);

            return ecResponse.ToResponse();
        }

        public DoExpressCheckoutResponse Do(DoExpressCheckoutRequest details)
        {
            var paymentDetail = new PaymentDetailsType();
            paymentDetail.PaymentAction = PaymentActionCodeType.SALE;
            paymentDetail.OrderTotal = new BasicAmountType(CurrencyCodeType.NZD, details.Amount.ToCurrencyString());

            var request = new DoExpressCheckoutPaymentRequestType {Version = "104.0"};
            var requestDetails = new DoExpressCheckoutPaymentRequestDetailsType
            {
                PaymentDetails = paymentDetail.ToList(),
                Token = details.Token,
                PayerID = details.PayerId
            };
            request.DoExpressCheckoutPaymentRequestDetails = requestDetails;

            var wrapper = new DoExpressCheckoutPaymentReq {DoExpressCheckoutPaymentRequest = request};
            var service = new PayPalAPIInterfaceServiceService(GetConfig());
            var doECResponse = service.DoExpressCheckoutPayment(wrapper);

            return doECResponse.ToResponse();
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
            paymentItem.Amount = new BasicAmountType(CurrencyCodeType.NZD, details.Amount.ToCurrencyString());
            paymentItem.Quantity = 1;
            paymentItem.ItemCategory = ItemCategoryType.DIGITAL;
            var paymentItems = new List<PaymentDetailsItemType>();
            paymentItems.Add(paymentItem);
            paymentDetail.PaymentDetailsItem = paymentItems;

            paymentDetail.PaymentAction = PaymentActionCodeType.SALE;
            paymentDetail.OrderTotal = new BasicAmountType(CurrencyCodeType.NZD, details.Amount.ToCurrencyString());

            return paymentDetail.ToList();
        }
    }
}
