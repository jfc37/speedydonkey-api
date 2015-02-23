using Data.Repositories;
using Models;
using Validation.Validators;

namespace Validation.Tests.Builders
{
    public class EnrolStudentValidatorBuilder
    {
        private IPersonRepository<Student> _studentRepository;
        private ICourseRepository _courseRepository;

        public EnrolStudentValidatorBuilder WithStudentRepository(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            return this;
        }

        public EnrolStudentValidatorBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }

        public EnrolStudentValidator Build()
        {
            return new EnrolStudentValidator(_studentRepository, _courseRepository);
        }
    }
}