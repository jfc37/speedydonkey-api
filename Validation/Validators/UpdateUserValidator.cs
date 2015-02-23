using System.Linq;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateUserValidator : AbstractValidator<User>, IActionValidator<UpdateUser, User>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Id).Must(BeExistingUser).WithMessage(ValidationMessages.UserDoesntExist);
            RuleFor(x => x.Username).NotEmpty().WithMessage(ValidationMessages.MissingUsername);
            RuleFor(x => x.Username).Must(BeUnique).WithMessage(ValidationMessages.DuplicateUsername);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessages.MissingPassword);
        }

        private bool BeUnique(User user, string username)
        {
            return _userRepository.GetAll().All(x => x.Username != username || x.Id == user.Id);
        }

        private bool BeExistingUser(int userId)
        {
            return _userRepository.Get(userId) != null;
        }
    }
}
