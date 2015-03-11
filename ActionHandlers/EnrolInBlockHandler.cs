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

        public EnrolInBlockHandler(
            IRepository<User> userRepository,
            IRepository<Block> blockRepository)
        {
            _userRepository = userRepository;
            _blockRepository = blockRepository;
        }

        public User Handle(EnrolInBlock action)
        {
            var user = _userRepository.Get(action.ActionAgainst.Id);
            var blockBeingEnroledIn = _blockRepository.Get(action.ActionAgainst.EnroledBlocks.Single().Id);
            AddBlockToUser(user, blockBeingEnroledIn);
            return _userRepository.Update(user);
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
