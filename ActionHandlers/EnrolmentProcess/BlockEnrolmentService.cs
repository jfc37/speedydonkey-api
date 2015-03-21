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
            var blocksBeingEnroledIn = _blockRepository.GetAllWithChildren(new []{"Classes"})
                .Where(b => blockIds.Any(x => x == b.Id))
                .ToList();
            AddBlocksToUser(user, blocksBeingEnroledIn);
            AddClassesToUserSchedule(user, blocksBeingEnroledIn.SelectMany(x => x.Classes).ToList());
            AddUserToClassRoll(user, blocksBeingEnroledIn.SelectMany(x => x.Classes).ToList());
        }

        private void AddClassesToUserSchedule(User user, IList<IClass> classes)
        {
            if (user.Schedule == null)
            {
                user.Schedule = new List<IBooking>();
            }
            var bookings = _bookingRepository.GetAllWithChildren(new []{"Event"}).Where(x => classes.Select(c => c.Id).Contains(x.Event.Id));
            foreach (var booking in bookings)
            {
                user.Schedule.Add(booking);
            }
        }

        private void AddUserToClassRoll(User user, IList<IClass> classes)
        {
            foreach (var thisClass in classes)
            {
                if (thisClass.RegisteredStudents == null)
                    thisClass.RegisteredStudents = new List<IUser>();

                thisClass.RegisteredStudents.Add(user);
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