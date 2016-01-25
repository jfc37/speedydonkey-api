using Contracts.PrivateLessons;
using NodaTime;

namespace Contracts.MappingExtensions
{
    /// <summary>
    /// Mapping extensions between LocalTime and LocalTimeModel
    /// </summary>
    public static class LocalTimeMappingExtensions
    {
        /// <summary>
        /// Converts LocalTime to LocalTimeModel
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static LocalTimeModel ToModel(this LocalTime instance)
        {
            return new LocalTimeModel(instance.Hour, instance.Minute);
        }
        /// <summary>
        /// Converts LocalTimeModel to LocalTime
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static LocalTime ToEntity(this LocalTimeModel instance)
        {
            return new LocalTime(instance.Hour, instance.Minute);
        }
    }
}