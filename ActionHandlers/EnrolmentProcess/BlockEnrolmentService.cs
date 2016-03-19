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

        public BlockEnrolmentService(
            IRepository<Block> blockRepository)
        {
            _blockRepository = blockRepository;
        }

        public IList<Block> EnrolInBlocks(User user, IEnumerable<int> blockIds)
        {
            var blocksBeingEnroledIn = _blockRepository.Queryable()
                .Where(b => blockIds.Contains(b.Id))
                .ToList();

            AddBlocksToUser(user, blocksBeingEnroledIn);
            AddClassesToUserSchedule(user, blocksBeingEnroledIn.SelectMany(x => x.Classes));
            AddUserToClassRoll(user, blocksBeingEnroledIn.SelectMany(x => x.Classes));

            return blocksBeingEnroledIn;
        }

        private void AddClassesToUserSchedule(User user, IEnumerable<Class> classes)
        {
            if (user.Schedule == null)
            {
                user.Schedule = new List<Event>();
            }
            foreach (var theClass in classes)
            {
                user.Schedule.Add(theClass);
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