using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateCourseWorkGradeValidator : AbstractValidator<CourseWorkGrade>, IActionValidator<UpdateCourseWorkGrade, CourseWorkGrade>
    {
        private readonly ICourseWorkGradeRepository _courseWorkGradeRepository;

        public UpdateCourseWorkGradeValidator(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;

            RuleFor(x => x.Id).Must(BeExistingCourseWorkGrade).WithMessage(ValidationMessages.CourseWorkGradeDoesntExist);
        }

        private bool BeExistingCourseWorkGrade(CourseWorkGrade courseWorkGrade, int id)
        {
            return _courseWorkGradeRepository.Get(courseWorkGrade.CourseGrade.Student.Id, courseWorkGrade.CourseGrade.Course.Id, courseWorkGrade.CourseWork.Id) != null;
        }
    }
}
