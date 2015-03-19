using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.EnrolmentProcess
{
    public class EnrolInBlockHandler : IActionHandler<EnrolInBlock, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IBlockEnrolmentService _blockEnrolmentService;
        private readonly IUserPassAppender _userPassAppender;

        public EnrolInBlockHandler(
            IRepository<User> userRepository,
            IBlockEnrolmentService blockEnrolmentService,
            IUserPassAppender userPassAppender)
        {
            _userRepository = userRepository;
            _blockEnrolmentService = blockEnrolmentService;
            _userPassAppender = userPassAppender;
        }

        public User Handle(EnrolInBlock action)
        {
            var user = _userRepository.GetWithChildren(action.ActionAgainst.Id, new List<string> { "EnroledBlocks", "EnroledBlocks.Classes" });
            if (action.ActionAgainst.EnroledBlocks != null)
            {
                _blockEnrolmentService.EnrolInBlocks(user, action.ActionAgainst.EnroledBlocks.Select(x => x.Id).ToList());   
            }
            AddPassToUser(user, action);
            return _userRepository.Update(user);
        }

        private void AddPassToUser(User user, EnrolInBlock action)
        {
            if (action.ActionAgainst.Passes != null)
            {
                foreach (var pass in action.ActionAgainst.Passes)
                {
                    _userPassAppender.AddPassToUser(user, pass.PassType);
                }
            }
        }
    }

    public interface IUserPassAppender
    {
        void AddPassToUser(User user, string passType);
    }

    public class UserPassAppender : IUserPassAppender
    {
        private readonly IPassCreatorFactory _passCreatorFactory;

        public UserPassAppender(IPassCreatorFactory passCreatorFactory)
        {
            _passCreatorFactory = passCreatorFactory;
        }

        public void AddPassToUser(User user, string passType)
        {
            if (user.Passes == null)
                user.Passes = new List<IPass>();

            user.Passes.Add(_passCreatorFactory.Get(passType).CreatePass());
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
                return new ClipPassCreator();

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
                PassType = PassType.Unlimited.ToString()
            };
        }
    }

    public class ClipPassCreator : IPassCreator
    {
        public IPass CreatePass()
        {
            return new ClipPass
            {
                EndDate = DateTime.Now.Date.AddDays(6 * 7),
                StartDate = DateTime.Now.Date,
                PassType = PassType.Clip.ToString(),
                ClipsRemaining = 6
            };
        }
    }
}
