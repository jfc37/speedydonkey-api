using ActionHandlers.UserPasses;
using Common.Extensions;
using Data.Repositories;
using Models;
using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies.Pass
{
    public class PassStrategy : ITypedItemStrategy<PassTemplate>
    {
        private readonly IRepository<PassTemplate> _passTemplateRepository;
        private readonly IRepository<User> _userRepository;
        private readonly UserPassAppender _userPassAppender;

        public PassStrategy(
            IRepository<PassTemplate> passTemplateRepository,
            IRepository<User> userRepository,
            UserPassAppender userPassAppender)
        {
            _passTemplateRepository = passTemplateRepository;
            _userRepository = userRepository;
            _userPassAppender = userPassAppender;
        }

        public decimal GetPrice(string itemId)
        {
            return _passTemplateRepository.Get(itemId.ToInt()).Cost;
        }

        public string GetDescription(string itemId)
        {
            return _passTemplateRepository.Get(itemId.ToInt()).Description;
        }

        public void CompletePurchase(OnlinePayment completedPayment)
        {
            var passTemplate = _passTemplateRepository.Get(completedPayment.ItemId.ToInt());
            var user = _userRepository.Get(completedPayment.InitiatedBy);
            _userPassAppender.AddPassToUser(user, PassPaymentStatus.Paid.ToString(), passTemplate);

            _userRepository.Update(user);
        }
    }
}
