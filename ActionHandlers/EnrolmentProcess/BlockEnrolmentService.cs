using System;
using System.Collections.Generic;
using System.Linq;
using Data.Repositories;
using Models;

namespace ActionHandlers.EnrolmentProcess
{
    public interface IBlockEnrolmentService
    {
        IList<Block> EnrolInBlocks(User user, IEnumerable<int> blockIds);
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

        public IList<Block> EnrolInBlocks(User user, IEnumerable<int> blockIds)
        {
            var allBlocks = _blockRepository.GetAllWithChildren(new[] {"Classes"});
            var interestedBlocks = allBlocks.Where(b => blockIds.Any(x => x == b.Id));
            var blocksBeingEnroledIn = interestedBlocks.ToList();

            AddBlocksToUser(user, blocksBeingEnroledIn);
            AddClassesToUserSchedule(user, blocksBeingEnroledIn.SelectMany(x => x.Classes));
            AddUserToClassRoll(user, blocksBeingEnroledIn.SelectMany(x => x.Classes));

            return blocksBeingEnroledIn;
        }

        private void AddClassesToUserSchedule(User user, IEnumerable<Class> classes)
        {
            if (user.Schedule == null)
            {
                user.Schedule = new List<Booking>();
            }
            var bookings = classes.Select(x => new Booking
            {
                CreatedDateTime = DateTime.Now,
                Event = x
            }).ToList();
            foreach (var booking in bookings)
            {
                _bookingRepository.Create(booking);
                user.Schedule.Add(booking);
            }
        }

        private void AddUserToClassRoll(User user, IEnumerable<Class> classes)
        {
            foreach (var thisClass in classes)
            {
                if (thisClass.RegisteredStudents == null)
                    thisClass.RegisteredStudents = new List<User>();

                thisClass.RegisteredStudents.Add(user);
            }
        }

        private void AddBlocksToUser(User user, IEnumerable<Block> blockBeingEnroledIn)
        {
            if (user.EnroledBlocks == null)
            {
                user.EnroledBlocks = new List<Block>();
            }
            foreach (var block in blockBeingEnroledIn)
            {
                user.EnroledBlocks.Add(block);
            }
        }
    }
}