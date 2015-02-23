using Data.Repositories;

namespace Data.Tests.Builders
{
    public class CourseWorkRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CourseWorkRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CourseWorkRepository Build()
        {
            return new CourseWorkRepository(_context);
        }
    }
}