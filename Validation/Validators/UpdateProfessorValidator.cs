using System.Linq;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateProfessorValidator : AbstractValidator<Professor>, IActionValidator<UpdateProfessor, Professor>
    {
        private readonly IPersonRepository<Professor> _personRepository;

        public UpdateProfessorValidator(IPersonRepository<Professor> personRepository)
        {
            _personRepository = personRepository;

            RuleFor(x => x.Id).Must(BeExistingPerson).WithMessage(ValidationMessages.ProfessorDoesntExist);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(ValidationMessages.MissingFirstName);
            RuleFor(x => x.Surname).NotEmpty().WithMessage(ValidationMessages.MissingSurname);
        }

        private bool BeExistingPerson(int personId)
        {
            return _personRepository.Get(personId) != null;
        }
    }
}
