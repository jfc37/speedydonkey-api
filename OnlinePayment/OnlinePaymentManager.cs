using System;
using Common;
using Data.Repositories;
using Models.OnlinePayments;
using OnlinePayments.ItemStrategies;
using OnlinePayments.PaymentFeeStrategies;

namespace OnlinePayments
{
    public class OnlinePaymentManager : IOnlinePaymentManager
    {
        private readonly IItemStrategyFactory _itemStrategyFactory;
        private readonly IPaymentFeeStrategyFactory _feeStrategyFactory;
        private readonly IRepository<OnlinePayment> _repository;
        private readonly IItemValidationStrategyFactory _validationStrategyFactory;
        private readonly ICurrentUser _currentUser;

        public OnlinePaymentManager(
            IItemStrategyFactory itemStrategyFactory,
            IPaymentFeeStrategyFactory feeStrategyFactory,
            IRepository<OnlinePayment> repository,
            IItemValidationStrategyFactory validationStrategyFactory,
            ICurrentUser currentUser)
        {
            _itemStrategyFactory = itemStrategyFactory;
            _feeStrategyFactory = feeStrategyFactory;
            _repository = repository;
            _validationStrategyFactory = validationStrategyFactory;
            _currentUser = currentUser;
        }

        public TResponse Begin<TPayment, TResponse>(
            TPayment payment, 
            IStartPaymentStrategy<TPayment, TResponse> paymentStrategy,
            IResponseCreator<TResponse> responseCreator)
            where TPayment : OnlinePayment 
            where TResponse : IStartOnlinePaymentResponse
        {
            if (!IsItemValidToPurchase(payment))
            {
                var errorResponse = responseCreator.Create();
                errorResponse.AddError("Invalid item being paid for");
                return errorResponse;
            }

            var populatedOnlinePayment = GetPopulatedPayment(payment);

            var response = paymentStrategy.StartPayment(populatedOnlinePayment);

            _repository.Create(populatedOnlinePayment);
            
            return response;
        }

        public TResponse Complete<TPaymentId, TPayment, TResponse>(
            TPaymentId payment, 
            ICompletePaymentStrategy<TPaymentId, TResponse, TPayment> paymentStrategy) 
            where TPayment : OnlinePayment 
            where TResponse : ICompleteOnlinePaymentResponse
        {
            var completeResponse = paymentStrategy.CompletePayment(payment);
            if (completeResponse.IsValid)
            {
                var completedPayment = paymentStrategy.GetCompletedPayment(payment);
                completedPayment.PaymentStatus = OnlinePaymentStatus.Complete;
                _repository.Update(completedPayment);


                var strategy = _itemStrategyFactory.GetStrategy(completedPayment);
                strategy.CompletePurchase(completedPayment);
            }

            return completeResponse;
        }

        private bool IsItemValidToPurchase(OnlinePayment payment)
        {
            var validationStrategy = _validationStrategyFactory.GetStrategy(payment.ItemType);
            return validationStrategy.IsValid(payment.ItemId);
        }

        private TPayment GetPopulatedPayment<TPayment>(TPayment payment)
            where TPayment : OnlinePayment
        {
            var strategy = _itemStrategyFactory.GetStrategy(payment);
            payment.Price = strategy
                .GetPrice(payment.ItemId);

            payment.Description = strategy
                .GetDescription(payment.ItemId);

            payment.Fee = _feeStrategyFactory
                .GetPaymentFeeStrategy(payment.PaymentMethod)
                .GetFee(payment);

            payment.InitiatedBy = _currentUser.Id;
            payment.ReferenceNumber = Guid.NewGuid();

            return payment;
        }
    }
}
