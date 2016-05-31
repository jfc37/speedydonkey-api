using System.Collections.Generic;
using System.Linq;
using Action.Classes;
using Common;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Classes
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
                .Must(NotAlreadyBeAttendingClass).WithMessage(ValidationMessages.AlreadyAttending);
        }

        private bool NotAlreadyBeAttendingClass(Class theClass, ICollection<User> users)
        {
            var retrievedClass = GetClass(theClass.Id);
            if (retrievedClass == null)
                return true;
            var userId = users.Single().Id;
            return retrievedClass.ActualStudents == null || retrievedClass.ActualStudents.All(x => x.Id != userId);
        }

        private bool HaveAValidPass(ICollection<User> users)
        {
            var user = GetUser(users);
            return user.SelectMany(x => x.Passes)
                .Any(x => x.IsValid());
        }

        private bool BeExistingUser(ICollection<User> users)
        {
            var user = GetUser(users);
            return user != null;
        }

        private Option<User> GetUser(ICollection<User> users)
        {
            var user = _userRepository.Get(users.Single().Id)
                .ToOption();
            return user;
        }

        private Class GetClass(int id)
        {
            var theClass = _classRepository.Get(id);
            return theClass;
        }
    }
}
