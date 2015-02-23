using System.Linq;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class EnrolStudentHandler : IActionHandler<EnrolStudent, Student>
    {
        private readonly IPersonRepository<Student> _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrolStudentHandler(IPersonRepository<Student> studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public Student Handle(EnrolStudent action)
        {
            var student = _studentRepository.Get(action.ActionAgainst.Id);
            var courseToEnrolIn = _courseRepository.Get(action.ActionAgainst.EnroledCourses.Single().Id);
            student.EnroledCourses.Add(courseToEnrolIn);
            student.CourseGrades.Add(new CourseGrade{Course = courseToEnrolIn, Student = student});
            return _studentRepository.Update(student);
        }
    }
}
