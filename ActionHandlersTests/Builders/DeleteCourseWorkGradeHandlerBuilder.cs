using ActionHandlers;
using Data.Repositories;

namespace ActionHandlersTests.Builders
{
    public class DeleteCourseWorkGradeHandlerBuilder
    {
        private ICourseWorkGradeRepository _courseWorkGradeRepository;

        public DeleteCourseWorkGradeHandler Build()
        {
            return new DeleteCourseWorkGradeHandler(_courseWorkGradeRepository);
        }

        public DeleteCourseWorkGradeHandlerBuilder WithCourseWorkGradeRepository(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
            return this;
        }
    }
}