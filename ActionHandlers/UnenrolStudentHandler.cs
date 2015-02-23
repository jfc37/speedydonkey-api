using System.Linq;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UnenrolStudentHandler : IActionHandler<UnenrolStudent, Student>
    {
        private readonly IPersonRepository<Student> _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public UnenrolStudentHandler(IPersonRepository<Student> studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public Student Handle(UnenrolStudent action)
        {
            int courseIdToUnenrol = action.ActionAgainst.EnroledCourses.Single().Id;

            var student = _studentRepository.Get(action.ActionAgainst.Id);
            var courseToUnenrolIn = _courseRepository.Get(courseIdToUnenrol);
            student.EnroledCourses.Remove(courseToUnenrolIn);

            var courseGradeToUnenrolIn = student.CourseGrades.Single(x => x.Course.Id == courseIdToUnenrol);
            student.CourseGrades.Remove(courseGradeToUnenrolIn);
            _studentRepository.Update(student);
            return null;
        }
    }
}
