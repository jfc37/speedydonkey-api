namespace Models.OnlinePayments
{
    public class PayPalPayment : OnlinePayment
    {
        public virtual string ReturnUrl { get; set; }
        public virtual string CancelUrl { get; set; }
        public virtual string BuyerEmail { get; set; }
        public virtual string Token { get; set; }
    }

    public interface IStartOnlinePaymentResponse
    {
        bool IsValid { get; }

    }

}
