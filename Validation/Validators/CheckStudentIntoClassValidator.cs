using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators
{
    public class CheckStudentIntoClassValidator : AbstractValidator<Class>, IActionValidator<CheckStudentIntoClass, Class>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Class> _classRepository;

        public CheckStudentIntoClassValidator(IRepository<User> userRepository, IRepository<Class> classRepository)
        {
            _userRepository = userRepository;
            _classRepository = classRepository;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id).Must(x => new DoesIdExist<Class>(classRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidClass);
            RuleFor(x => x.ActualStudents)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(x => new HasExactlyOneInSetRule(x).IsValid()).WithMessage(ValidationMessages.IncorrectNumberOfAttendees)
                .Must(BeExistingUser).WithMessage(ValidationMessages.InvalidUser)
                .Must(HaveAValidPass).WithMessage(ValidationMessages.NoValidPasses)
                .Must(HavePaidForAPass).WithMessage(ValidationMessages.NoPaidForPasses)
                .Must(NotAlreadyBeAttendingClass).WithMessage(ValidationMessages.AlreadyAttendingClass);
        }

        private bool NotAlreadyBeAttendingClass(Class theClass, ICollection<IUser> users)
        {
            var retrievedClass = GetClass(theClass.Id);
            if (retrievedClass == null)
                return true;
            var userId = users.Single().Id;
            return retrievedClass.ActualStudents == null || retrievedClass.ActualStudents.All(x => x.Id != userId);
        }

        private bool HaveAValidPass(ICollection<IUser> users)
        {
            var user = GetUser(users);
            return user.Passes != null && user.Passes.Any(x => x.IsValid());
        }

        private bool HavePaidForAPass(ICollection<IUser> users)
        {
            var user = GetUser(users);
            return user.Passes != null && user.Passes.Any(x => x.PaymentStatus == PassPaymentStatus.Paid.ToString());
        }

        private bool BeExistingUser(ICollection<IUser> users)
        {
            var user = GetUser(users);
            return user != null;
        }

        private User GetUser(ICollection<IUser> users)
        {
            var user = _userRepository.Get(users.Single().Id);
            return user;
        }

        private Class GetClass(int id)
        {
            var theClass = _classRepository.Get(id);
            return theClass;
        }
    }
}
