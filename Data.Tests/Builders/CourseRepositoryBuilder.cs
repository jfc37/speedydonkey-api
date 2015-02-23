using Data.Repositories;

namespace Data.Tests.Builders
{
    public class CourseRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CourseRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CourseRepository Build()
        {
            return new CourseRepository(_context);
        }
    }
}