using System.Linq;
using Data.Repositories;
using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetOpeningHours
{
    /// <summary>
    /// Updates opening hours
    /// </summary>
    /// <seealso cref="IOpeningHourManager" />
    public class UpdateOpeningHourManager : IOpeningHourManager
    {
        private readonly OpeningHours _openingHours;
        private readonly IRepository<OpeningHours> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOpeningHourManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="openingHours">The opening hours.</param>
        public UpdateOpeningHourManager(
            IRepository<OpeningHours> repository, 
            OpeningHours openingHours)
        {
            _repository = repository;
            _openingHours = openingHours;
        }

        /// <summary>
        /// Updates an opening hour
        /// </summary>
        /// <returns></returns>
        public OpeningHours Save()
        {
            var original = _repository.GetAll()
                .Single(x => x.Day == _openingHours.Day);

            original.OpeningTime = _openingHours.OpeningTime;
            original.ClosingTime = _openingHours.ClosingTime;

            return _repository.Update(original);
        }
    }
}