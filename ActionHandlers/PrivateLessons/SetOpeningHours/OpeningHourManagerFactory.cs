using System.Linq;
using Data.Repositories;
using Models.PrivateLessons;

namespace ActionHandlers.PrivateLessons.SetOpeningHours
{
    /// <summary>
    /// Factory for OpeningHourManagers
    /// </summary>
    /// <seealso cref="IOpeningHourManagerFactory" />
    public class OpeningHourManagerFactory : IOpeningHourManagerFactory
    {
        private readonly IRepository<OpeningHours> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningHourManagerFactory"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public OpeningHourManagerFactory(IRepository<OpeningHours> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Constructs the correct type of OpeningHourManager
        /// </summary>
        /// <param name="openingHours">The opening hours.</param>
        /// <returns></returns>
        public IOpeningHourManager GetManager(OpeningHours openingHours)
        {
            var alreadyExists = _repository.GetAll()
                .Any(x => x.Day == openingHours.Day);

            return alreadyExists
                ? (IOpeningHourManager) new UpdateOpeningHourManager(_repository, openingHours)
                : new CreateOpeningHourManager(_repository, openingHours);
        }
    }
}