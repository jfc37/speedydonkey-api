using System.Linq;
using Actions;
using Data;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateUserValidator : AbstractValidator<User>, IActionValidator<CreateUser, User>
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public CreateUserValidator(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            RuleFor(x => x.Username).NotEmpty().WithMessage(ValidationMessages.MissingUsername);
            RuleFor(x => x.Username).Must(BeUnique).WithMessage(ValidationMessages.DuplicateUsername);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidationMessages.MissingPassword);
        }

        private bool BeUnique(string username)
        {
            return _context.Users.All(x => x.Username != username);
        }
    }
}
