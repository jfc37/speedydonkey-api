using System.Linq;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;
using Models.Settings;

namespace ActionHandlers.Teachers.TeacherRates
{
    /// <summary>
    /// Factory for Teacher Rates
    /// </summary>
    public class TeacherRateFactory : ITeacherRateFactory
    {
        private readonly IRepository<SettingItem> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherRateFactory"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public TeacherRateFactory(IRepository<SettingItem> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new teacher rate with default rates.
        /// </summary>
        /// <returns></returns>
        public TeacherRate CreateDefault()
        {
            var soloRate = GetSoloRate();
            var partneredRate = GetPartneredRate();

            return new TeacherRate(soloRate, partneredRate);
        }

        private decimal GetSoloRate()
        {
            return GetRate(SettingTypes.TeacherRateSolo, 40);
        }

        private decimal GetPartneredRate()
        {
            return GetRate(SettingTypes.TeacherRatePartnered, 30);
        }

        private decimal GetRate(SettingTypes type, decimal defaultRate)
        {
            return _repository.Queryable()
                .Where(x => x.Name == type)
                .Select(x => x.Value.ParseToDecimal())
                .ConvertSetToOption()
                .DefaultIfEmpty(defaultRate)
                .Single();
        }
    }
}