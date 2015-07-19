namespace OnlinePayment.Models
{
    public class PaypalError
    {
        public string ShortMessage { get; set; }
        public string LongMessage { get; set; }
        public string ErrorCode { get; set; }
        public string SeverityCode { get; set; }   
    }
}