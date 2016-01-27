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
        /// Converts OpeningHours to TimeSlotModel
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static TimeSlotModel ToModel(this OpeningHours instance)
        {
            return instance.IsNull() 
                ? null 
                : new TimeSlotModel(instance.Day, instance.OpeningTime.ToModel(), instance.ClosingTime.ToModel());
        }

        /// <summary>
        /// Converts TimeSlotModel to OpeningHours
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static OpeningHours ToEntity(this TimeSlotModel instance)
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