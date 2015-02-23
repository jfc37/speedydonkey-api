using Data.Repositories;

namespace Data.Tests.Builders
{
    public class StudentRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public StudentRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public StudentRepository Build()
        {
            return new StudentRepository(_context);
        }
    }
}