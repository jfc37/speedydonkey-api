using System.Data.Entity;
using System.Linq;
using Actions;
using Data;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreatePersonValidator : AbstractValidator<Person>, IActionValidator<CreatePerson, Person>
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public CreatePersonValidator(ISpeedyDonkeyDbContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName).NotEmpty().WithMessage(ValidationMessages.MissingFirstName);
            RuleFor(x => x.Surname).NotEmpty().WithMessage(ValidationMessages.MissingSurname);
            RuleFor(x => x.User).NotNull().WithMessage(ValidationMessages.NoLinkedUser);

            RuleFor(x => x.User).Must(AlreadyExist).When(x => x.User != null).WithMessage(ValidationMessages.UserDoesntExist);
            RuleFor(x => x.User).Must(NotHavePersonLinked).When(x => x.User != null).WithMessage(ValidationMessages.LinkedUserAlreadyHasPerson);
        }

        private bool NotHavePersonLinked(User user)
        {
            var users = _context.Users
                .Include(x => x.Person);
                
                
            return users.All(x => x.Id != user.Id || x.Person == null);
        }

        private bool AlreadyExist(User user)
        {
            return _context.Users.Any(x => x.Id == user.Id);
        }
    }
}
