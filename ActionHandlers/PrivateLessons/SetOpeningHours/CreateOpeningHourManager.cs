using Data.Repositories;
using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetOpeningHours
{
    /// <summary>
    /// Creates opening hours
    /// </summary>
    /// <seealso cref="IOpeningHourManager" />
    public class CreateOpeningHourManager : IOpeningHourManager
    {
        private readonly OpeningHours _openingHours;
        private readonly IRepository<OpeningHours> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOpeningHourManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="openingHours">The opening hours.</param>
        public CreateOpeningHourManager(
            IRepository<OpeningHours> repository, 
            OpeningHours openingHours)
        {
            _repository = repository;
            _openingHours = openingHours;
        }

        /// <summary>
        /// Creates an opening hour
        /// </summary>
        /// <returns></returns>
        public OpeningHours Save()
        {
            return _repository.Create(_openingHours);
        }
    }
}
