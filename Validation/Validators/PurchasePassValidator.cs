using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators
{
    public class PurchasePassValidator : AbstractValidator<PurchasePass>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ICurrentUser _currentUser;

        public PurchasePassValidator(
            IRepository<User> userRepository, 
            IRepository<PassTemplate> passTemplateRepository,
            ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.PassTemplateId)
                .Must(x => new DoesIdExist<PassTemplate>(passTemplateRepository, x).IsValid())
                .WithMessage(ValidationMessages.InvalidPassType);

            RuleFor(x => x.ActionAgainst.Id)
                .Must(x => new DoesIdExist<User>(userRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidUser)
                .Must(HavePermissionToAddPass).WithMessage(ValidationMessages.CannotAddPassForAnother);

            RuleFor(x => x.ActionAgainst.Passes).NotEmpty().WithMessage(ValidationMessages.ProvidePasses)
                .Must(HavePermissionForPaymentStatus).WithMessage(ValidationMessages.CannontAddPaidPass);
        }

        private bool HavePermissionForPaymentStatus(PurchasePass action, IList<Pass> passes)
        {
            if (passes.Single().PaymentStatus == PassPaymentStatus.Pending.ToString())
                return true;

            var retrievedUser = _userRepository.Get(_currentUser.Id);
            return new DoesUserHaveClaimRule(retrievedUser, Claim.Teacher)
                .IsValid();
        }

        private bool HavePermissionToAddPass(int id)
        {
            if (id == _currentUser.Id)
                return true;

            var user = _userRepository.Get(_currentUser.Id);
            return new DoesUserHaveClaimRule(user, Claim.Teacher)
                .IsValid();
        }
    }
}
