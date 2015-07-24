﻿using Models.OnlinePayments;

namespace OnlinePayments
{
    public interface IOnlinePaymentManager
    {
        TResponse Begin<TPayment, TResponse>(
            TPayment payment, 
            IStartPaymentStrategy<TPayment, TResponse> paymentStrategy,
            IResponseCreator<TResponse> responseCreator)
            where TPayment : OnlinePayment
            where TResponse : IStartOnlinePaymentResponse;
    }
}