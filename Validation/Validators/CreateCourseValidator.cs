using System;
using System.Linq;
using Actions;
using Data;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateCourseValidator : AbstractValidator<Course>, IActionValidator<CreateCourse, Course>
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public CreateCourseValidator(ISpeedyDonkeyDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.MissingName);
            RuleFor(x => x.Name).Must(BeUnique).WithMessage(ValidationMessages.DuplicateCourseName);
            RuleFor(x => x.StartDate).GreaterThan(DateTime.Today.AddYears(-1)).WithMessage(ValidationMessages.MissingStartDate);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateBeforeStartDate);
            RuleFor(x => x.GradeType).NotEqual(GradeType.Invalid).WithMessage(ValidationMessages.MissingGradeType);
        }

        private bool BeUnique(string courseName)
        {
            return _context.Courses.All(x => x.Name != courseName);
        }
    }
}
