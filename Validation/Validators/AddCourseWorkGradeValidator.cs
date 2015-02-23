using System.Collections.Generic;
using System.Linq;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class AddCourseWorkGradeValidator : AbstractValidator<CourseWorkGrade>, IActionValidator<AddCourseWorkGrade, CourseWorkGrade>
    {
        private readonly IPersonRepository<Student> _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public AddCourseWorkGradeValidator(IPersonRepository<Student> studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;

            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(x => x.CourseGrade.Student.Id).Must(BeExistingStudent).WithMessage(ValidationMessages.StudentDoesntExist);
            RuleFor(x => x.CourseGrade.Course).Must(BeExistingCourse).WithMessage(ValidationMessages.CoursesDontExist);

            When(x => BeExistingStudent(x.CourseGrade.Student.Id) && BeExistingCourse(x.CourseGrade.Course),
                () =>
                {
                    RuleFor(x => x.CourseWork).Must(NotAlreadyHaveGradeForCourseWork).WithMessage(ValidationMessages.StudentAlreadyHasGradeForCourseWork);
                    RuleFor(x => x.CourseWork)
                        .Must(BeExistingCourseWork)
                        .WithMessage(ValidationMessages.CourseWorkDoesntExist);
                });

        }

        private bool NotAlreadyHaveGradeForCourseWork(CourseWorkGrade courseWorkGrade, CourseWork courseWork)
        {
            IList<CourseGrade> currentGrades = _studentRepository.Get(courseWorkGrade.CourseGrade.Student.Id).CourseGrades;
            return !currentGrades.Any() || currentGrades.All(x => x.CourseWorkGrades.All(y => y.CourseWork.Id != courseWork.Id));
        }

        private bool BeExistingCourseWork(CourseWorkGrade courseWorkGrade, CourseWork courseWork)
        {
            var courseInDatabase = _courseRepository.Get(courseWorkGrade.CourseGrade.Course.Id);
            return courseInDatabase.Assignments.Any(x => x.Id == courseWork.Id) || courseInDatabase.Exams.Any(x => x.Id == courseWork.Id);
        }

        private bool BeExistingCourse(Course course)
        {
            return _courseRepository.Get(course.Id) != null;
        }

        private bool BeExistingStudent(int studentId)
        {
            return _studentRepository.Get(studentId) != null;
        }
    }
}
