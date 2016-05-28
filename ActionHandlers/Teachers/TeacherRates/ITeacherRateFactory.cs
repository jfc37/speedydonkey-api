using Models;

namespace ActionHandlers.Teachers.TeacherRates
{
    /// <summary>
    /// Factory for Teacher Rates
    /// </summary>
    public interface ITeacherRateFactory
    {
        /// <summary>
        /// Creates a new teacher rate with default rates.
        /// </summary>
        /// <returns></returns>
        TeacherRate CreateDefault();
    }
}