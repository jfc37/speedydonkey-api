using ActionHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlersTests.Builders
{
    public class EnrolStudentHandlerBuilder
    {
        private IPersonRepository<Student> _studentRepository;
        private ICourseRepository _courseRepository;

        public EnrolStudentHandlerBuilder WithStudentRepository(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            return this;
        }

        public EnrolStudentHandlerBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }

        public EnrolStudentHandler Build()
        {
            return new EnrolStudentHandler(_studentRepository, _courseRepository);
        }
    }
}