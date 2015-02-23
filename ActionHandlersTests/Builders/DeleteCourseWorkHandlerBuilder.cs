using ActionHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlersTests.Builders
{
    public class DeleteCourseWorkHandlerBuilder
    {
        private ICourseWorkRepository<CourseWork> _courseWorkRepository;

        public DeleteCourseWorkHandler Build()
        {
            return new DeleteCourseWorkHandler(_courseWorkRepository);
        }

        public DeleteCourseWorkHandlerBuilder WithCourseWorkRepository(ICourseWorkRepository<CourseWork> courseWorkRepository)
        {
            _courseWorkRepository = courseWorkRepository;
            return this;
        }
    }
}