﻿using System.Linq;
using Action.OnlinePayment;
using Data.Repositories;
using Models;
using OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;

namespace ActionHandlers.OnlinePayments
{
    public class ConfirmOnlinePaymentHandler : IActionHandlerWithResult<ConfirmOnlinePayment, PendingOnlinePayment, PayPalConfirmResponse>
    {
        private readonly IExpressCheckout _expressCheckout;
        private readonly IRepository<PendingOnlinePayment> _repository;

        public ConfirmOnlinePaymentHandler(
            IExpressCheckout expressCheckout,
            IRepository<PendingOnlinePayment> repository)
        {
            _expressCheckout = expressCheckout;
            _repository = repository;
        }

        public PayPalConfirmResponse Handle(ConfirmOnlinePayment action)
        {
            var result = _expressCheckout.Get(action.ActionAgainst.Token);

            var pendingOnlinePayment = _repository.GetAll().Single(x => x.Token == action.ActionAgainst.Token);
            pendingOnlinePayment.PayerId = result.PayerId;
            _repository.Update(pendingOnlinePayment);

            result.Description = pendingOnlinePayment.Description;
            result.Amount = pendingOnlinePayment.Amount;
            return result;
        }
    }
}
