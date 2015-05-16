using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using Models;
using Notification;
using Notification.Notifications;

namespace ActionHandlers.EnrolmentProcess
{
    public class EnrolInBlockHandler : IActionHandler<EnrolInBlock, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IBlockEnrolmentService _blockEnrolmentService;
        private readonly IPostOffice _postOffice;

        public EnrolInBlockHandler(
            IRepository<User> userRepository,
            IBlockEnrolmentService blockEnrolmentService, 
            IPostOffice postOffice)
        {
            _userRepository = userRepository;
            _blockEnrolmentService = blockEnrolmentService;
            _postOffice = postOffice;
        }

        public User Handle(EnrolInBlock action)
        {
            IList<Block> blocks = new List<Block>();
            var user = _userRepository.GetWithChildren(action.ActionAgainst.Id, new List<string> { "EnroledBlocks", "EnroledBlocks.Classes" });
            if (action.ActionAgainst.EnroledBlocks != null)
            {
                blocks = _blockEnrolmentService.EnrolInBlocks(user, action.ActionAgainst.EnroledBlocks.Select(x => x.Id).ToList()); 
            }
            SendEmail(user, blocks);
            return _userRepository.Update(user);
        }

        private void SendEmail(User user, IList<Block> blocksBeingEnroledIn)
        {
            var userNotication = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Surname = user.Surname,
                EnroledBlocks = blocksBeingEnroledIn.Select(x => (IBlock) x).ToList(),
                Passes = null
            };
            var notification = new UserEnroledInBlock(userNotication);
            _postOffice.Send(notification);
        }
    }
}
