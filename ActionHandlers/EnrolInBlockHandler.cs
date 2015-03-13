using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class EnrolInBlockHandler : IActionHandler<EnrolInBlock, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Block> _blockRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public EnrolInBlockHandler(
            IRepository<User> userRepository,
            IRepository<Block> blockRepository,
            IRepository<Booking> bookingRepository)
        {
            _userRepository = userRepository;
            _blockRepository = blockRepository;
            _bookingRepository = bookingRepository;
        }

        public User Handle(EnrolInBlock action)
        {
            var user = _userRepository.Get(action.ActionAgainst.Id);
            var blockBeingEnroledIn = _blockRepository.Get(action.ActionAgainst.EnroledBlocks.Single().Id);
            AddBlockToUser(user, blockBeingEnroledIn);
            AddClassesToUserSchedule(user, blockBeingEnroledIn.Classes);
            return _userRepository.Update(user);
        }

        private void AddClassesToUserSchedule(User user, IList<IClass> classes)
        {
            if (user.Schedule == null)
            {
                user.Schedule = new List<IBooking>();
            }
            var bookings = _bookingRepository.GetAll().Where(x => classes.Select(c => c.Id).Contains(x.Event.Id));
            foreach (var booking in bookings)
            {
                user.Schedule.Add(booking);
            }
        }

        private void AddBlockToUser(User user, Block blockBeingEnroledIn)
        {
            if (user.EnroledBlocks == null)
            {
                user.EnroledBlocks = new List<IBlock>();
            }
            user.EnroledBlocks.Add(blockBeingEnroledIn);
        }
    }
}
