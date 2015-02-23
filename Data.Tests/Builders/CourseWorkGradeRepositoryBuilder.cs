using Data.Repositories;

namespace Data.Tests.Builders
{
    public class CourseWorkGradeRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CourseWorkGradeRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CourseWorkGradeRepository Build()
        {
            return new CourseWorkGradeRepository(_context);
        }
    }
}