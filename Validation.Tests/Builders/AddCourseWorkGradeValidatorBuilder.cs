using Data.Repositories;
using Models;
using Validation.Validators;

namespace Validation.Tests.Builders
{
    public class AddCourseWorkGradeValidatorBuilder
    {
        private IPersonRepository<Student> _studentRepository;
        private ICourseRepository _courseRepository;

        public AddCourseWorkGradeValidatorBuilder WithStudentRepository(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            return this;
        }

        public AddCourseWorkGradeValidatorBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }

        public AddCourseWorkGradeValidator Build()
        {
            return new AddCourseWorkGradeValidator(_studentRepository, _courseRepository);
        }
    }
}