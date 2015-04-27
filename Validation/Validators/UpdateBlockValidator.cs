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
        private readonly IRepository<Teacher> _teacherRepository;

        public UpdateBlockValidator(IRepository<Block> repository, IRepository<Teacher> teacherRepository)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
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

        private bool BeExistingTeachers(IList<ITeacher> teachers)
        {
            foreach (var teacher in teachers)
            {
                var savedTeacher = _teacherRepository.Get(teacher.Id);
                if (savedTeacher == null || !savedTeacher.Claims.Contains(Claim.Teacher.ToString()))
                    return false;
            }
            return true;
        }
    }
}
