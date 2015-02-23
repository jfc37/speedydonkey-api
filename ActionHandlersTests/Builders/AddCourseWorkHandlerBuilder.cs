using ActionHandlers;
using Data.Repositories;

namespace ActionHandlersTests.Builders
{
    public class AddCourseWorkGradeHandlerBuilder
    {
        private ICourseWorkGradeRepository _courseWorkGradeRepository;

        public AddCourseWorkGradeHandlerBuilder WithCourseWorkGradeRepository(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
            return this;
        }

        public AddCourseWorkGradeHandler Build()
        {
            return new AddCourseWorkGradeHandler(_courseWorkGradeRepository);
        }
    }
}