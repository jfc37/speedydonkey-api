using Common.Calculations;

namespace ActionHandlers.Calculations
{
    public class ExpressCheckoutTotalCalculation : ICalculation<decimal>
    {
        private readonly decimal _amount;

        public ExpressCheckoutTotalCalculation(decimal amount)
        {
            _amount = amount;
        }

        /// <summary>
        /// 3.4% + $0.45 per transaction
        /// </summary>
        /// <returns></returns>
        public CalculationResult<decimal> Calculate()
        {
            decimal fee = new ExpressCheckoutFeeCalculation(_amount)
                .Calculate()
                .Result();
            decimal total = _amount + fee;

            return new CalculationResult<decimal>(total);
        }
    }
}