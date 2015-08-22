using Common.Calculations;

namespace OnlinePayments.PaymentMethods.Poli
{
    public class PoliFeeCalculation : ICalculation<decimal>
    {
        private const decimal PercentageFee = 1m;
        private readonly decimal _amount;

        public PoliFeeCalculation(decimal amount)
        {
            _amount = amount;
        }

        public CalculationResult<decimal> Calculate()
        {
            decimal percentageOfTransaction = _amount * PercentageFee / 100;

            return new CalculationResult<decimal>(percentageOfTransaction);
        }
    }
}