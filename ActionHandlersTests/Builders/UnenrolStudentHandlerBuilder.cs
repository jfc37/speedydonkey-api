using ActionHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlersTests.Builders
{
    public class UnenrolStudentHandlerBuilder
    {
        private IPersonRepository<Student> _studentRepository;
        private ICourseRepository _courseRepository;

        public UnenrolStudentHandlerBuilder WithStudentRepository(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            return this;
        }

        public UnenrolStudentHandlerBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }

        public UnenrolStudentHandler Build()
        {
            return new UnenrolStudentHandler(_studentRepository, _courseRepository);
        }
    }
}