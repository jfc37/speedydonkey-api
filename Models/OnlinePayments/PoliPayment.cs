namespace Models.OnlinePayments
{
    public class PoliPayment : OnlinePayment
    {
        public virtual string SuccessUrl { get; set; }
        public virtual string FailureUrl { get; set; }
        public virtual string CancellationUrl { get; set; }
        public virtual string PoliId { get; set; }
        public virtual string Token { get; set; }
    }
}
