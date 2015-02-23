using ActionHandlers;
using Data.Repositories;

namespace ActionHandlersTests.Builders
{
    public class DeleteCourseHandlerBuilder
    {
        private ICourseRepository _courseRepository;

        public DeleteCourseHandler Build()
        {
            return new DeleteCourseHandler(_courseRepository);
        }

        public DeleteCourseHandlerBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }
    }
}