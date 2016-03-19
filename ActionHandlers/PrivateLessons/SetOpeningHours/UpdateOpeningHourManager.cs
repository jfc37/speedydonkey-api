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
        private readonly TimeSlot _timeSlot;
        private readonly IRepository<TimeSlot> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOpeningHourManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="timeSlot">The opening hours.</param>
        public UpdateOpeningHourManager(
            IRepository<TimeSlot> repository, 
            TimeSlot timeSlot)
        {
            _repository = repository;
            _timeSlot = timeSlot;
        }

        /// <summary>
        /// Updates an opening hour
        /// </summary>
        /// <returns></returns>
        public TimeSlot Save()
        {
            var original = _repository.Queryable()
                .Single(x => x.Day == _timeSlot.Day);

            original.OpeningTime = _timeSlot.OpeningTime;
            original.ClosingTime = _timeSlot.ClosingTime;

            return _repository.Update(original);
        }
    }
}