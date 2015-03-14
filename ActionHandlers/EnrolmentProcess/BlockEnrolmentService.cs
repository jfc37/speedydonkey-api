using System.Collections.Generic;
using System.Linq;
using Data.Repositories;
using Models;

namespace ActionHandlers.EnrolmentProcess
{
    public interface IBlockEnrolmentService
    {
        void EnrolInBlocks(User user, IList<int> blockIds);
    }

    public class BlockEnrolmentService : IBlockEnrolmentService
    {
        private readonly IRepository<Block> _blockRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public BlockEnrolmentService(
            IRepository<Block> blockRepository,
            IRepository<Booking> bookingRepository)
        {
            _blockRepository = blockRepository;
            _bookingRepository = bookingRepository;
        }

        public void EnrolInBlocks(User user, IList<int> blockIds)
        {
            var blocksBeingEnroledIn = _blockRepository.GetAll()
                .Where(b => blockIds.Any(x => x == b.Id))
                .ToList();
            AddBlocksToUser(user, blocksBeingEnroledIn);
            AddClassesToUserSchedule(user, blocksBeingEnroledIn.SelectMany(x => x.Classes).ToList());
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

        private void AddBlocksToUser(User user, IList<Block> blockBeingEnroledIn)
        {
            if (user.EnroledBlocks == null)
            {
                user.EnroledBlocks = new List<IBlock>();
            }
            foreach (var block in blockBeingEnroledIn)
            {
                user.EnroledBlocks.Add(block);
            }
        }
    }
}