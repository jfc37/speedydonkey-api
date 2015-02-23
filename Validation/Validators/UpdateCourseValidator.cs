using System;
using System.Linq;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateCourseValidator : AbstractValidator<Course>, IActionValidator<UpdateCourse, Course>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseValidator(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;

            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.MissingName);
            RuleFor(x => x.Id).Must(BeExistingCourse).WithMessage(ValidationMessages.CourseDoesntExist);
            RuleFor(x => x.Name).Must(BeUnique).WithMessage(ValidationMessages.DuplicateCourseName);
            RuleFor(x => x.StartDate).GreaterThan(DateTime.Today.AddYears(-1)).WithMessage(ValidationMessages.MissingStartDate);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateBeforeStartDate);
            RuleFor(x => x.GradeType).NotEqual(GradeType.Invalid).WithMessage(ValidationMessages.MissingGradeType);
        }

        private bool BeUnique(Course course, string name)
        {
            var allCourses = _courseRepository.GetAll();
            return allCourses.All(x => x.Name != name || x.Id == course.Id);
        }

        private bool BeExistingCourse(int courseId)
        {
            return _courseRepository.Get(courseId) != null;
        }
    }
}
