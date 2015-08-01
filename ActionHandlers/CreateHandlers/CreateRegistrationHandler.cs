using System;
using Actions;
using Data.Repositories;
using Models;
using Models.OnlinePayments;

namespace ActionHandlers.CreateHandlers
{
    public class CreateRegistrationHandler : CreateEntityHandler<CreateRegistration, Registration>
    {
        public CreateRegistrationHandler(IRepository<Registration> repository)
            : base(repository)
        {
        }

        protected override void PreHandle(ICrudAction<Registration> action)
        {
            action.ActionAgainst.RegistationId = Guid.NewGuid();
            action.ActionAgainst.PaymentStatus = OnlinePaymentStatus.Pending;
            action.ActionAgainst.Amount = new decimal(199.99);
        }
    }
}
