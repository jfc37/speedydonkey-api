using Data.Repositories;
using Validation.Validators;

namespace Validation.Tests.Builders
{
    public class UpdateCourseWorkGradeValidatorBuilder
    {
        private ICourseWorkGradeRepository _courseWorkGradeRepository;

        public UpdateCourseWorkGradeValidator Build()
        {
            return new UpdateCourseWorkGradeValidator(_courseWorkGradeRepository);
        }

        public UpdateCourseWorkGradeValidatorBuilder WithCourseWorkGradeRepository(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
            return this;
        }
    }
}