using System.Linq;
using Common.Extensions;
using Contracts.PrivateLessons;
using Models.PrivateLessons;

namespace Contracts.MappingExtensions
{
    /// <summary>
    /// Mapping extensions for teacher availability
    /// </summary>
    public static class TeacherAvailablityMappingExtensions
    {
        /// <summary>
        /// Converts TeacherAvailability to TeacherAvailabilityModel
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static TeacherAvailabilityModel ToModel(this TeacherAvailability instance)
        {
            return instance.IsNull() 
                ? null 
                : new TeacherAvailabilityModel(
                    instance.Availabilities.Select(x => OpeningHoursMappingExtensions.ToModel(x)),
                    instance.Teacher.ToStripedModel());
        }

        /// <summary>
        /// Converts TeacherAvailabilityModel to TeacherAvailability
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static TeacherAvailability ToEntity(this TeacherAvailabilityModel instance)
        {
            if (instance.IsNull())
                return null;

            return new TeacherAvailability(
                instance.Availabilities.Select(x => x.ToEntity()),
                instance.Teacher.ToEntity());
        }
    }
}