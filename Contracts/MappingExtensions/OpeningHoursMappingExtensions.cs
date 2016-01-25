using Common.Extensions;
using Contracts.PrivateLessons;
using Models.PrivateLessons;

namespace Contracts.MappingExtensions
{
    /// <summary>
    /// Mapping extensions for opening hours
    /// </summary>
    public static class OpeningHoursMappingExtensions
    {
        /// <summary>
        /// Converts OpeningHours to OpeningHoursModel
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static OpeningHoursModel ToModel(this OpeningHours instance)
        {
            return instance.IsNull() 
                ? null 
                : new OpeningHoursModel(instance.Day, instance.OpeningTime.ToModel(), instance.ClosingTime.ToModel());
        }

        /// <summary>
        /// Converts OpeningHoursModel to OpeningHours
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static OpeningHours ToEntity(this OpeningHoursModel instance)
        {
            if (instance.IsNull())
                return null;

            return new OpeningHours
            {
                Day = instance.Day,
                OpeningTime = instance.OpeningTime.ToEntity(),
                ClosingTime = instance.ClosingTime.ToEntity()
            };
        }
    }
}