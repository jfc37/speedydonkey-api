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
        private readonly TimeSlot _timeSlot;
        private readonly IRepository<TimeSlot> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOpeningHourManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="timeSlot">The opening hours.</param>
        public CreateOpeningHourManager(
            IRepository<TimeSlot> repository, 
            TimeSlot timeSlot)
        {
            _repository = repository;
            _timeSlot = timeSlot;
        }

        /// <summary>
        /// Creates an opening hour
        /// </summary>
        /// <returns></returns>
        public TimeSlot Save()
        {
            return _repository.Create(_timeSlot);
        }
    }
}
