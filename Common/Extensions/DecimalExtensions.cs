namespace Common.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToCurrencyString(this decimal instance)
        {
            return instance.ToString("N2");
        }
    }
}