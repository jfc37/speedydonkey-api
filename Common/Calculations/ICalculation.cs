namespace Common.Calculations
{
    public interface ICalculation<T>
    {
        CalculationResult<T> Calculate();
    }
}
