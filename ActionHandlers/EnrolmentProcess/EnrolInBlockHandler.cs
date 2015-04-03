using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using ActionHandlers.UserPasses;
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
        private readonly IUserPassAppender _userPassAppender;
        private readonly IPostOffice _postOffice;

        public EnrolInBlockHandler(IRepository<User> userRepository, IBlockEnrolmentService blockEnrolmentService, IUserPassAppender userPassAppender, IPostOffice postOffice)
        {
            _userRepository = userRepository;
            _blockEnrolmentService = blockEnrolmentService;
            _userPassAppender = userPassAppender;
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
            AddPassToUser(user, action);
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

        private void AddPassToUser(User user, EnrolInBlock action)
        {
            if (action.ActionAgainst.Passes != null)
            {
                foreach (var pass in action.ActionAgainst.Passes)
                {
                    _userPassAppender.AddPassToUser(user, pass);
                }
            }
        }
    }

    public interface IPassCreatorFactory
    {
        IPassCreator Get(string passType);
    }

    public class PassCreatorFactory : IPassCreatorFactory
    {
        public IPassCreator Get(string passType)
        {
            if (passType == PassType.Unlimited.ToString())
                return new UnlimitedPassCreator();

            if (passType == PassType.Clip.ToString())
                return new ClipPassCreator(6);

            if (passType == PassType.Single.ToString())
                return new ClipPassCreator(1);

            throw new ArgumentException(String.Format("Don't know how to create passes of type {0}", passType));
        }
    }

    public interface IPassCreator
    {
        IPass CreatePass();
    }

    public class UnlimitedPassCreator : IPassCreator
    {
        public IPass CreatePass()
        {
            return new Pass
            {
                EndDate = DateTime.Now.Date.AddDays(6 * 7),
                StartDate = DateTime.Now.Date,
                PassType = PassType.Unlimited.ToString(),
                PaymentStatus = PassPaymentStatus.Pending.ToString()
            };
        }
    }

    public class ClipPassCreator : IPassCreator
    {
        private readonly int _numberofClips;

        public ClipPassCreator(int numberOfClips)
        {
            _numberofClips = numberOfClips;
        }

        public IPass CreatePass()
        {
            return new ClipPass
            {
                EndDate = DateTime.Now.Date.AddDays(6 * 7),
                StartDate = DateTime.Now.Date,
                PassType = PassType.Clip.ToString(),
                ClipsRemaining = _numberofClips
            };
        }
    }
}
