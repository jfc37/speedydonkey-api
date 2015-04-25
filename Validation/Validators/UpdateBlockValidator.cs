using System.Collections.Generic;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateBlockValidator : AbstractValidator<Block>, IActionValidator<UpdateBlock, Block>
    {
        private readonly IRepository<Block> _repository;
        private readonly IRepository<User> _userRepository;

        public UpdateBlockValidator(IRepository<Block> repository, IRepository<User> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Id)
                .Must(Exist).WithMessage(ValidationMessages.InvalidBlock);

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(BeExistingTeachers).WithMessage(ValidationMessages.InvalidTeachers);
        }

        private bool Exist(int id)
        {
            return _repository.Get(id) != null;
        }

        private bool BeExistingTeachers(IList<IUser> teachers)
        {
            foreach (var teacher in teachers)
            {
                var savedTeacher = _userRepository.Get(teacher.Id);
                if (savedTeacher == null || !savedTeacher.Claims.Contains(Claim.Teacher.ToString()) || savedTeacher.TeachingConcerns == null)
                    return false;
            }
            return true;
        }
    }
}
