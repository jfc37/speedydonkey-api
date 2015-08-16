using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.UserPasses
{
    public class PurchasePassHandler : IActionHandler<PurchasePass, User>
    {
        private readonly IUserPassAppender _userPassAppender;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<PassTemplate> _passTemplateRepository;

        public PurchasePassHandler(
            IUserPassAppender userPassAppender,
            IRepository<User> userRepository,
            IRepository<PassTemplate> passTemplateRepository)
        {
            _userPassAppender = userPassAppender;
            _userRepository = userRepository;
            _passTemplateRepository = passTemplateRepository;
        }

        public User Handle(PurchasePass action)
        {
            var passTemplate = _passTemplateRepository.Get(action.PassTemplateId);
            if (passTemplate == null)
                return null;

            var user = _userRepository.Get(action.ActionAgainst.Id);
            var pass = action.ActionAgainst.Passes.Single();
            _userPassAppender.AddPassToUser(user, pass.PaymentStatus, passTemplate);
            return _userRepository.Update(user);
        }
    }
}
