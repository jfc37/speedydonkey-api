using Action;
using Common.Extensions;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Blocks
{
    public class CreateBlockValidator : AbstractValidator<Block>, IActionValidator<CreateBlock, Block>
    {
        private readonly IRepository<Teacher> _teacherRepository;

        public CreateBlockValidator(IRepository<Teacher> teacherRepository, IRepository<Room> roomRepository)
        {
            _teacherRepository = teacherRepository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.StartDate)
                .Must(x => new DateIsNotTooFarInThePastRule(x.Date).IsValid()).WithMessage(ValidationMessages.MissingStartTime);

            RuleFor(x => x.MinutesPerClass)
                .GreaterThan(0).WithMessage(ValidationMessages.InvalidClassMinutes);

            RuleFor(x => x.NumberOfClasses)
                .GreaterThan(0).WithMessage(ValidationMessages.InvalidClassesInBlock);

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(x => new AreTeachersValidRule(x, _teacherRepository).IsValid()).WithMessage(ValidationMessages.InvalidTeachers);

            RuleFor(x => x.Room)
                .Must(x => new DoesIdExist<Room>(roomRepository, x.Id).IsValid()).WithMessage(ValidationMessages.InvalidRoom)
                .When(x => x.Room.IsNotNull());
        }
    }
}
