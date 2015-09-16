﻿using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators
{
    public class PurchasePassValidator : AbstractValidator<User>, IActionValidator<PurchasePass, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ICurrentUser _currentUser;

        public PurchasePassValidator(IRepository<User> userRepository, ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<User>(userRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidUser)
                .Must(HavePermissionToAddPass).WithMessage(ValidationMessages.CannotAddPassForAnother);

            RuleFor(x => x.Passes).NotEmpty().WithMessage(ValidationMessages.ProvidePasses)
                .Must(HavePermissionForPaymentStatus).WithMessage(ValidationMessages.CannontAddPaidPass);
        }

        private bool HavePermissionForPaymentStatus(User user, IList<Pass> passes)
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
