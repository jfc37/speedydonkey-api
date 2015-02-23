using System.Linq;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateStudentValidator : AbstractValidator<Student>, IActionValidator<UpdateStudent, Student>
    {
        private readonly IPersonRepository<Student> _personRepository;

        public UpdateStudentValidator(IPersonRepository<Student> personRepository)
        {
            _personRepository = personRepository;

            RuleFor(x => x.Id).Must(BeExistingPerson).WithMessage(ValidationMessages.StudentDoesntExist);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(ValidationMessages.MissingFirstName);
            RuleFor(x => x.Surname).NotEmpty().WithMessage(ValidationMessages.MissingSurname);
        }

        private bool BeExistingPerson(int personId)
        {
            return _personRepository.Get(personId) != null;
        }
    }
}
