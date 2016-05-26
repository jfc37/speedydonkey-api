using Common.Extensions;
using Models;

namespace Contracts.Teachers
{
    /// <summary>
    /// Extensions for mapping TeacherRates and TeacherRateModels
    /// </summary>
    public static class TeacherRateMappingExtensions
    {
        /// <summary>
        /// Converts TeacherRate to TeacherRateModel.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static TeacherRateModel ToRateModel(this Teacher instance)
        {
            instance.GuardAgainstNull(nameof(instance));

            return new TeacherRateModel(instance.Rate.SoloRate, instance.Rate.PartnerRate, instance.Id, instance.User.FullName);
        }

        /// <summary>
        /// Converts TeacherRateModel to TeacherRate.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static TeacherRate ToEntity(this TeacherRateModel instance)
        {
            if (instance.IsNull())
                return null;

            return new TeacherRate(instance.SoloRate, instance.PartnerRate);
        }
    }
}