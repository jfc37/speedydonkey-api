using ActionHandlers;
using Data.Repositories;

namespace ActionHandlersTests.Builders
{
    public class CreateCourseHandlerBuilder
    {
        private ICourseRepository _courseRepository;

        public CreateCourseHandler Build()
        {
            return new CreateCourseHandler(_courseRepository);
        }

        public CreateCourseHandlerBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }
    }
}