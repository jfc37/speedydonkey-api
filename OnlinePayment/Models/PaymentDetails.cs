namespace OnlinePayment.Models
{
    public class PaymentDetails
    {
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string BuyerEmail { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

    }
}