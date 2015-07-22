using System;
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

        public OnlinePaymentManager(
            IItemStrategyFactory itemStrategyFactory,
            IPaymentFeeStrategyFactory feeStrategyFactory,
            IRepository<OnlinePayment> repository)
        {
            _itemStrategyFactory = itemStrategyFactory;
            _feeStrategyFactory = feeStrategyFactory;
            _repository = repository;
        }

        public TResponse Begin<TPayment, TResponse>(TPayment payment, IStartPaymentStrategy<TPayment, TResponse> paymentStrategy) where TPayment : OnlinePayment where TResponse : IStartOnlinePaymentResponse
        {
            var itemStrategy = _itemStrategyFactory.GetStrategy(payment.ItemType);
            if (!itemStrategy.GetValidationStrategy().IsValid())
                throw new Exception();

            var populatedOnlinePayment = PopulateOnlinePayment(payment, itemStrategy);

            var response = paymentStrategy.StartPayment(populatedOnlinePayment);

            _repository.Create(populatedOnlinePayment);
            
            return response;
        }

        private TPayment PopulateOnlinePayment<TPayment>(TPayment request, IItemStrategy strategy) where TPayment : OnlinePayment
        {
            request.Price = strategy
                .GetPriceStrategy()
                .GetPrice(request.ItemId);

            request.Description = strategy
                .GetDescriptionStrategy()
                .GetDescription();

            request.Fee = _feeStrategyFactory
                .GetPaymentFeeStrategy(request.PaymentMethod)
                .GetFee(request);

            return request;
        }
    }
}
