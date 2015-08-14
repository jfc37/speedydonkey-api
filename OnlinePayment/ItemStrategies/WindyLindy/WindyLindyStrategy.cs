using Common.Extensions;
using Data.CodeChunks;
using Data.Repositories;
using Models;
using Models.OnlinePayments;
using Notification.NotificationHandlers;
using Notification.Notifications;

namespace OnlinePayments.ItemStrategies.WindyLindy
{
    public class WindyLindyStrategy : ITypedItemStrategy<Registration>
    {
        private readonly IRepository<Registration> _repository;
        private readonly INotificationHandler<WindyLindyRegistrationCompletion> _notificationHandler;

        public WindyLindyStrategy(
            IRepository<Registration> repository, 
            INotificationHandler<WindyLindyRegistrationCompletion> notificationHandler)
        {
            _repository = repository;
            _notificationHandler = notificationHandler;
        }

        public decimal GetPrice(string itemId)
        {
            return GetRegistration(itemId)
                .Amount;
        }

        public string GetDescription(string itemId)
        {
            var registration = GetRegistration(itemId);
            return registration.FullPass.GetValueOrDefault()
                ? "Full Windy Lindy Pass" 
                : "Partial Windy Lindy Pass";
        }

        public void CompletePurchase(OnlinePayment completedPayment)
        {
            var registration = GetRegistration(completedPayment.ItemId);

            registration.PaymentStatus = OnlinePaymentStatus.Complete;
            _repository.Update(registration);

            _notificationHandler.Handle(new WindyLindyRegistrationCompletion(registration));
        }

        private Registration GetRegistration(string itemId)
        {
            return new GetRegistrationFromRegistrationNumber(_repository, itemId.ToGuid())
                .Do();
        }
    }
}