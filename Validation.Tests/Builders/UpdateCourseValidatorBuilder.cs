using Data.Repositories;
using Validation.Validators;

namespace Validation.Tests.Builders
{
    public class UpdateCourseValidatorBuilder
    {
        private ICourseRepository _courseRepository;

        public UpdateCourseValidator Build()
        {
            return new UpdateCourseValidator(_courseRepository);
        }

        public UpdateCourseValidatorBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }
    }
}