using Action;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators
{
    public class UpdateBlockValidator : AbstractValidator<Block>, IActionValidator<UpdateBlock, Block>
    {
        public UpdateBlockValidator(IRepository<Block> repository, IRepository<Teacher> teacherRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Block>(repository, x).IsValid()).WithMessage(ValidationMessages.InvalidBlock);

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(x => new AreTeachersValidRule(x, teacherRepository).IsValid()).WithMessage(ValidationMessages.InvalidTeachers);
        }
    }
}
