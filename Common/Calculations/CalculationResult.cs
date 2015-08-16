namespace Common.Calculations
{
    public class CalculationResult<T>
    {
        private readonly T _result;

        public CalculationResult(T result)
        {
            _result = result;
        }

        public T Result()
        {
            return _result;
        }
    }
}
