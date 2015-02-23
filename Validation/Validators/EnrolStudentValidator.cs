using System.Collections.Generic;
using System.Linq;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class EnrolStudentValidator : AbstractValidator<Student>, IActionValidator<EnrolStudent, Student>
    {
        private readonly IPersonRepository<Student> _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrolStudentValidator(IPersonRepository<Student> studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;

            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(x => x.Id).Must(BeExistingStudent).WithMessage(ValidationMessages.StudentDoesntExist);
            RuleFor(x => x.EnroledCourses).Must(x => x.Count == 1).WithMessage(ValidationMessages.EnrolingInMultipleCourses);
            RuleFor(x => x.EnroledCourses).Must(BeExistingCourses).WithMessage(ValidationMessages.CoursesDontExist);

            When(x => BeExistingStudent(x.Id) && BeExistingCourses(x.EnroledCourses), () => RuleFor(x => x.Id).Must(NotBeEnrolledInCourseAlready).WithMessage(ValidationMessages.StudentAlreadyEnroled));

        }

        private bool NotBeEnrolledInCourseAlready(Student student, int studentId)
        {
            var courseId = student.EnroledCourses.Select(x => x.Id).FirstOrDefault();
            IList<Course> currentlyEnroledCourses = _studentRepository.Get(studentId).EnroledCourses;
            return !currentlyEnroledCourses.Any() || currentlyEnroledCourses.All(x => x.Id != courseId);
        }

        private bool BeExistingCourses(IList<Course> courses)
        {
            return courses.All(x => _courseRepository.Get(x.Id) != null);
        }

        private bool BeExistingStudent(int studentId)
        {
            return _studentRepository.Get(studentId) != null;
        }
    }
}
